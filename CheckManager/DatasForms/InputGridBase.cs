using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Cells = SourceGrid.Cells;
using SourceGrid;
using SSIT.EncodeBase;
using SSIT.QMBase;
using SSIT.QM.CheckInterface;

namespace SSIT.QM.CheckManager.DatasForms
{
    public struct InputField
    {
        public string ComputeString;
        public string FieldName;
        public string StandardStr;
        public int ID;
        public bool ReadOnly;
        public ValueTypeEnum ValueType;
        public string CheckType;
        public float QualifyRate;
        public int Precision;
    }

    public class InputGridBase:SourceGrid.Grid
    {
        SourceGrid.Cells.Editors.TextBoxNumeric eNum;
        SourceGrid.Cells.Editors.TextBoxNumeric eNumReadOnly;

        SourceGrid.Cells.Editors.ComboBox eLogic;
        SourceGrid.Cells.Editors.ComboBox eSelection;
        public List<InputField> Fields { get; set; }
        public CheckOrder Order { get; set; }
        public int SelectedRow
        {
            get
            {
                int[] rows = Selection.GetRowsIndex();
                if (rows != null && rows.Length > 0)
                    return rows[0];
                return -1;
            }
            set
            {
                this.Selection.Clear();
                if (value >= FixedColumns && value < RowsCount)
                    this.Selection.SelectRow(value, true);
            }
        }

        public int SampleCount = 3;
        public int HeaderCount = 6;
        public InputGridBase()
            : base()
        {
            SSIT.PropertyBase.Resources res = new SSIT.PropertyBase.Resources();
            eNum = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(string));          
            eNum.Control.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
            eNum.EditableMode = res.editablemode;
            eNum.Control.ImeMode = System.Windows.Forms.ImeMode.Off;
            eNumReadOnly = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(string));
            eNumReadOnly.Control.ReadOnly = true;
            eNumReadOnly.EditableMode = EditableMode.None;

            eLogic = res.EncodeComboBox(new string[] { EncodeConst.RightMark, EncodeConst.WrongMark }, false);

            FixedColumns = FixedRows = 1;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            if (Controller.FindController(typeof(UpdateRowController)) == null)
                Controller.AddController(new UpdateRowController());
        }

        public virtual void Init()
        {
            SourceGrid.Cells.Views.Cell v_Normal = new SSIT.PropertyBase.Resources().V_Common;
            SourceGrid.Cells.Views.Cell v_ReadOnly = new SSIT.PropertyBase.Resources().V_Common;
            v_ReadOnly.BackColor = System.Drawing.SystemColors.Control;
            Redim(Fields.Count+FixedRows,HeaderCount + SampleCount+2);
            this[0, 0] = new Cells.Header("序号");
            this[0, 1] = new Cells.Header("检验项目");
            this[0, 2] = new Cells.Header("项目类型");
            this[0, 3] = new Cells.Header("值类型");
            this[0, 4] = new Cells.Header("检验标准");
            this[0, 5] = new Cells.Header("合格率要求(%)");

            for (int i = 0; i < Fields.Count; i++)
            {
                int rowindex = i + FixedRows;
                this[rowindex, 1] = new Cells.Header(Fields[i].FieldName);
                this[rowindex, 2] = new Cells.Header(Fields[i].CheckType);
                this[rowindex, 3] = new Cells.Header(ValueTypeClass.GetStringValue(Fields[i].ValueType));
                this[rowindex, 4] = new Cells.Header(Fields[i].StandardStr);
                this[rowindex, 5] = new Cells.Header(Fields[i].QualifyRate);
                this[rowindex, 0] = new Cells.RowHeader(i+1);
                this[rowindex, 0].Tag = Fields[i];
            }
            for (int i = 0; i < SampleCount; i++)
            {
                this[ 0,i+HeaderCount] = new Cells.ColumnHeader(string.Format("第 {0} 个样本",i+1));

            }
             SSIT.PropertyBase.Resources res = new SSIT.PropertyBase.Resources();

            for (int j = 0; j < Fields.Count; j++)
            {
                if (Fields[j].ValueType == ValueTypeEnum.Selection )
                {
                    if (!string.IsNullOrWhiteSpace(Fields[j].StandardStr))
                    {
                        string[] strs = Fields[j].StandardStr.Split(new char[] { '|', '|' });// SSITEncode.Common.STRING.Split(Fields[j].StandardStr, "||");
                        List<string> list = new List<string>();
                        if (strs != null)
                            foreach (string str in strs)
                            {
                                if (!string.IsNullOrWhiteSpace(str))
                                {
                                    list.Add(str);
                                }
                            }
                        eSelection = res.EncodeComboBox(list, false);
                    }
                    else
                    {
                        eSelection = res.EncodeComboBox(new string[]{string.Empty},false);
                    }
                }  
                for (int i = HeaderCount; i < SampleCount + HeaderCount; i++)
                {
                    Cells.Cell cell = new SourceGrid.Cells.Cell();

                    if (!Fields[j].ReadOnly)
                    {
                        switch (Fields[j].ValueType)
                        {
                            case ValueTypeEnum.Logic:
                                cell.Editor = eLogic;
                                break;
                            case ValueTypeEnum.Number:
                                cell.Editor = eNum;
                                break;
                            case ValueTypeEnum.Selection:
                                cell.Editor = eSelection;
                                break;
                        }
                        //cell.View = v_Normal;
                    }
                    else
                    {
                        cell.Editor = eNumReadOnly;
                       // cell.View = v_ReadOnly;
                    }
                    cell.View = new SSIT.PropertyBase.Resources().V_Standard;
                    this[j + FixedRows, i] = cell;
                }
            }
            //合格率 合格判定
            this[0, HeaderCount+SampleCount] = new Cells.Header("合格率(%)");
            this[0, HeaderCount + SampleCount+1] = new Cells.Header("合格判定");
            for (int i = 0; i < Fields.Count; i++)
            {
                int rowindex = i + FixedRows;
                this[rowindex, HeaderCount + SampleCount] = new Cells.Header();
                this[rowindex, HeaderCount + SampleCount+1] = new Cells.Header();
            }
            this.Columns.AutoSize(true);
            
            this.Selection.SelectionMode = SourceGrid.GridSelectionMode.Cell;
        }

        public void LoadDatas(EncodeCollection<CheckData> datas)
        {
            for (int i = 0; i < Fields.Count; i++)
            {                
                int rowindex = i + FixedRows;
                for(int sampleindex =1;sampleindex<=SampleCount;sampleindex++)
                {
                    CheckData sd = CheckData.FindData(datas,Order.CheckOrderID,Fields[i].ID,sampleindex);

                    if(sd == null){
                        sd = CheckData.CreateItem(Order.CheckOrderID,Fields[i].ID,sampleindex);
                        sd.SampleID = Order.SampleID;
                    }
                    else{
                        if (sampleindex == 1)
                        {
                            if (!CheckData.UseNewstStandard)
                            {
                                this[rowindex, 4].Value = sd.StandardStr;
                            }
                        }
                        this[rowindex,HeaderCount + sampleindex-1].Value = sd.DataValue;
                    }
                    sd.ValueType = Fields[i].ValueType;
                    var cell = this[rowindex,HeaderCount + sampleindex-1];
                    SetCellColor(cell, sd);
                    cell.Tag = sd;
                }
                
            }
        }

        public virtual bool UpdateRow(int Row, int Col)
        {
            SourceGrid.Cells.ICell cell = this[Row + FixedRows, Col + FixedColumns];
            string value = (string)cell.Value;
            string Value = string.Empty;
            switch (Fields[Row].ValueType)
            {
                case ValueTypeEnum.Number:

                    float fv;
                    if (!float.TryParse(value, out fv))
                    {
                         cell.Value = "";
                         Value = string.Empty;
                    }
                    else
                    {
                        Value = fv.ToString("f" + Fields[Row].Precision);
                    }
                    break;
                case ValueTypeEnum.Logic:
                case ValueTypeEnum.Selection:
                    Value = value;
                    if (Value == null)
                        Value = string.Empty;
                    break;
                    
            }
            CheckData data = (CheckData)cell.Tag;
            
            if (data.DataValue != Value)
            {
                data.DataValue = Value;
                //data.State = DataState.Changed;
               ReturnValue rv = UpdateItem(data,Fields[Row].StandardStr);
               if (rv.Success)
               {
                   bool bFinished = false;
                   for (int index = 0; index < SampleCount; index++)
                   {
                       string str = (string)this[Row + FixedRows, index + HeaderCount].Value;
                       if (!string.IsNullOrWhiteSpace(str))
                       {
                           bFinished = true;
                           break;
                       }
                   }
                   //SampleItemOrder.SetFinishedCheckItem(Order, data.CheckItemID, bFinished);
                   SetCellColor(cell, data);
               }
            }
            return true;
        }

        public void SetCellColor(Cells.ICell cell, CheckData sd)
        {
            if (sd.IsFalse)
            {
                cell.View.BackColor = Color.OrangeRed;
            }
            else
            {
                cell.View.BackColor = Color.White;
            }
        }

        public bool IsRealTimeUpdate = true;
        protected ReturnValue UpdateItem(CheckData item,string StandardStr)
        {
            item.UpdateTime = DateTime.Now.ToString(EncodeConst.DateTimeFormat);
            item.SetPass(StandardStr);
            item.State = DataState.New;
            if (IsRealTimeUpdate)
            {
                EncodeCollection<CheckData> ec = new EncodeCollection<CheckData>(item);
                return  Encode.EncodeData.SaveDatas<CheckData>(ec);
            }
            return new ReturnValue(true);
        }

        public ReturnValue SetAllLogicPass()
        {
            for (int Row = 0; Row < Fields.Count; Row++)
            {
                ReturnValue rv = SetLogicPass(Row);
                if (!rv.Success)
                    return rv;
            }
            return new ReturnValue(true);
        }

        public ReturnValue SetLogicPass(int Row)
        {
            try
            {
                if (Fields[Row].ValueType == ValueTypeEnum.Logic)
                {
                    for (int index = 0; index < SampleCount; index++)
                    {
                        this[Row + FixedRows, index + HeaderCount].Value = EncodeConst.RightMark;
                        UpdateRow(Row, index + HeaderCount - FixedColumns);
                    }
                }
                return new ReturnValue(true);
            }
            catch(Exception err)
            {
                return new ReturnValue(false, -1, "设置失败：" + err.Message);
            }
                       
        }
        #region 合格判定
        public QualifyJudgeEnum QualifyJudgeAll()
        {
            List<QualifyJudgeEnum> results = new List<QualifyJudgeEnum>();
            for(int Row =0;Row<Fields.Count;Row++)
            {
               QualifyJudgeEnum result= QualifyJudge(Row);
               switch (result)
               {
                   case QualifyJudgeEnum.UnJudge:
                       this[Row + FixedRows, SampleCount + HeaderCount].Value = "不判定";
                       this[Row + FixedRows, SampleCount + HeaderCount + 1].Value = "不判定";
                       break;
                   case QualifyJudgeEnum.UnFinish:
                       this[Row + FixedRows, SampleCount + HeaderCount].Value = "未完成";
                       this[Row + FixedRows, SampleCount + HeaderCount + 1].Value = "未完成";
                       break;
                   case QualifyJudgeEnum.Pass:
                       this[Row + FixedRows, SampleCount + HeaderCount + 1].Value = "合格";
                       break;
                   case QualifyJudgeEnum.False:
                       this[Row + FixedRows, SampleCount + HeaderCount + 1].Value = "不合格";
                       break;

               }
               results.Add(result);
            }
            if (results.Contains(QualifyJudgeEnum.UnFinish))
                return QualifyJudgeEnum.UnFinish;
            if (results.Contains(QualifyJudgeEnum.False))
                return QualifyJudgeEnum.False;
            if (results.Contains(QualifyJudgeEnum.UnJudge) || !results.Contains(QualifyJudgeEnum.Pass))
                return QualifyJudgeEnum.UnJudge;
            return QualifyJudgeEnum.Pass;
        }

        public QualifyJudgeEnum QualifyJudge(int Row)
        {
            InputField field = Fields[Row];
            if (string.IsNullOrWhiteSpace(field.StandardStr))
            {
                return QualifyJudgeEnum.UnJudge;
            }
            if(field.ValueType == ValueTypeEnum.Selection)
            {
                return QualifyJudgeEnum.UnJudge;
            }
           // List<string> values = new List<string>();
            List<CheckData> datas = new List<CheckData>();
            bool bUnFinish = true;
            int passcount = 0;
            for(int index =0;index<SampleCount;index++)
            {
                SourceGrid.Cells.ICell cell = this[Row + FixedRows, index + HeaderCount];
                //string value = (string)cell.Value;
                CheckData data = (CheckData)cell.Tag;
                if (!string.IsNullOrWhiteSpace(data.DataValue))
                {
                    bUnFinish = false;               
                    //return QualifyJudgeEnum.UnFinish; 
                    datas.Add(data);
                    bool isfalse = data.IsFalse;
                    data.SetPass(field.StandardStr);
                    if (isfalse != data.IsFalse)
                    {
                        data.State = DataState.Changed;                       
                        Encode.EncodeData.SaveDatas<CheckData>(new EncodeCollection<CheckData>(data));
                    }
                    SetCellColor(cell, data);
                    if (!data.IsFalse)
                    {
                        passcount++;
                    }
                    //values.Add(data);
                   // break;
                }
            }
            if (bUnFinish)
            {
                return QualifyJudgeEnum.UnFinish;
            }
            //int passcount = 0;
            //if (!string.IsNullOrWhiteSpace(field.StandardStr))
            //{
            //    //SSIT.Expression.ECompileExpression ece = SSIT.Expression.ECompileExpression.GetInstance(field.StandardStr);
            //    foreach (CheckData data in datas)
            //    {
            //        data.SetPass(field.StandardStr);
            //        if (!data.IsFalse)
            //        {
            //            passcount++;
            //        }
            //        //switch (field.ValueType)
            //        //{
            //        //    case ValueTypeEnum.Number:
            //        //        if (ece.CheckValue(value) == Expression.ExpressionResult.TRUE)
            //        //            passcount++;
            //        //        break;
            //        //    case ValueTypeEnum.Logic:
            //        //        if(value == EncodeConst.RightMark)
            //        //        {
            //        //            passcount++;
            //        //        }
            //        //        break;
            //        //}
            //    }
            //}

            float rate =100f* passcount / datas.Count;
            this[Row + FixedRows, SampleCount + HeaderCount].Value = rate.ToString("f2");
            if (rate >= field.QualifyRate)
            {
                return QualifyJudgeEnum.Pass;
            }
            else
            {
                return QualifyJudgeEnum.False;
            }
        }
        #endregion

        //上下左右都能移动
        void Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (eNum.EditCellContext != null)
            {
                Position p = eNum.EditCellContext.Position;
                if (e.KeyCode == System.Windows.Forms.Keys.Up || e.KeyCode == System.Windows.Forms.Keys.Down
                 || e.KeyCode == System.Windows.Forms.Keys.Left || e.KeyCode == System.Windows.Forms.Keys.Right)
                {
                    eNum.EditCellContext.EndEdit(false);
                    switch (e.KeyCode)
                    {
                        case System.Windows.Forms.Keys.Up:
                            if (p.Row > FixedRows)
                            {
                                this.SetFocusCell(this[p.Row - 1, p.Column]);
                            }
                            break;
                        case System.Windows.Forms.Keys.Down:
                            if (p.Row < RowsCount - 1)
                            {
                                this.SetFocusCell(this[p.Row + 1, p.Column]);
                            }
                            break;
                        case System.Windows.Forms.Keys.Left:
                            if (p.Column > FixedColumns)
                            {
                                this.SetFocusCell(this[p.Row, p.Column - 1]);
                            }
                            break;
                        case System.Windows.Forms.Keys.Right:
                            if (p.Column < ColumnsCount - 1)
                            {
                                this.SetFocusCell(this[p.Row, p.Column + 1]);
                            }
                            break;
                    }
                }
            }

        }

        private class UpdateRowController : Cells.Controllers.ControllerBase
        {
            bool bEnterClick = false;
            public override void OnEditEnded(CellContext sender, System.EventArgs e)
            {
                base.OnEditEnded(sender, e);
                InputGridBase grid = (InputGridBase)sender.Grid;
                
                //if (grid[sender.Position.Row, sender.Position.Column].Value == null)
                //    return;
                grid.UpdateRow(sender.Position.Row-grid.FixedRows,sender.Position.Column-grid.FixedColumns);

                //grid.Columns.AutoSize(true);
                //grid.Rows.AutoSize(true);
                //SourceGrid.Grid grid = (SourceGrid.Grid)sender.Grid;
                if (bEnterClick)
                {
                    if (sender.Position.Row < grid.RowsCount - 1)
                        grid[sender.Position.Row + 1, sender.Position.Column].Focus();
                    else if (sender.Position.Column < grid.ColumnsCount - 1)
                        grid[grid.FixedRows, sender.Position.Column + 1].Focus();
                    bEnterClick = false;
                }
                //if (sender.Position.Column < grid.Columns.Count - 1)
                //    grid[sender.Position.Row, sender.Position.Column + 1].Focus();
                //else if (sender.Position.Row < grid.Rows.Count - 1)
                //    grid[sender.Position.Row + 1, grid.FixedColumns].Focus();

            }

            public override void OnKeyDown(CellContext sender, System.Windows.Forms.KeyEventArgs e)
            {
                base.OnKeyDown(sender, e);
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    bEnterClick = true;
                }
                else
                {
                    bEnterClick = false;
                }
            }
        }

        protected class NextColController : Cells.Controllers.ControllerBase
        {
            public override void OnEditEnded(CellContext sender, System.EventArgs e)
            {
                base.OnEditEnded(sender, e);
                
                SourceGrid.Grid grid = (SourceGrid.Grid)sender.Grid;
                if (sender.Position.Column < grid.Columns.Count - 1)
                    grid[sender.Position.Row, sender.Position.Column + 1].Focus();
                else if (sender.Position.Row < grid.Rows.Count - 1)
                    grid[sender.Position.Row + 1, grid.FixedColumns].Focus();

            }


        }
        protected class MyHeader : SourceGrid.Cells.ColumnHeader
        {
            public static int FontHeight = 10;
            public MyHeader(object value)
                : base(value)
            {
                //1 Header Row
                SourceGrid.Cells.Views.ColumnHeader view = new SourceGrid.Cells.Views.ColumnHeader();
                view.Font = new Font(FontFamily.GenericSansSerif, FontHeight, FontStyle.Bold);
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                View = view;

                //ResizeEnabled = false;
                AutomaticSortEnabled = false;
                //Console.WriteLine ("ColHeader");
            }
        }
    }
}
