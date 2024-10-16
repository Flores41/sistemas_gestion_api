using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Drawing2D;
using BudgetForcast.API.DTO;
using System.Collections;

namespace Aplication.Util
{
    public static class Util
    {
        private static IConfiguration Configuration;

        public static string ParseString(object str)
        {

            if (str.GetType() == Type.GetType("System.Boolean"))
                return "System.Boolean";
            else if (str.GetType() == Type.GetType("System.Int32"))
                return "System.Int32";
            else if (str.GetType() == Type.GetType("System.Int64"))
                return "System.Int64";
            else if (str.GetType() == Type.GetType("System.Decimal"))
                return "System.Decimal";
            else if (str.GetType() == Type.GetType("System.DateTime"))
                return "System.DateTime";
            else if (str.GetType().IsGenericType && str.GetType().GetGenericTypeDefinition() == typeof(List<>))
                return "System.Collections.Generic.List";
            else return "System.String";
        }

        public static string saveFileToDisk(IFormFile file, string newNameFile, string rutaTemp)
        {
            string workingDirectory = Environment.CurrentDirectory + "\\" + rutaTemp;
            newNameFile = (newNameFile == "" ? file.FileName : newNameFile);

            string filePath = "";
            string ruta = workingDirectory;
            if (file.Length > 0)
            {
                filePath = Path.Combine(ruta, newNameFile);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return workingDirectory;
        }



        public static string deleteFileToDisk(IFormFile file, string newNameFile, string rutaTemp)
        {
            string workingDirectory = Environment.CurrentDirectory + "\\" + rutaTemp;
            newNameFile = (newNameFile == "" ? file.FileName : newNameFile);
            string filePath = "";
            string ruta = workingDirectory;
            if (file.Length > 0)
            {
                filePath = Path.Combine(ruta, newNameFile);
                File.Delete(filePath);
            }

            return filePath;
        }






        public static IWorkbook CreateExcel<X, T>(int IniciaFilaDetalle, int mostrarBordes, IWorkbook hssfwb, string rutaArchivo, string nombreHoja, X _model, List<T> model, String nombreReporte, string param, string modelplantilla, int iniciafila, int iniciaColumnaCabecera, string nombrehojaLista)
        {
            string fileName = nombreReporte + ".xlsx";
            string nomhoja = "";
            string nomhojaLista = "";
            int nrowsIni = 0;
            Int32 nrows = 0;

            nomhoja = "";
            nomhojaLista = "";


            if (nombreReporte != "")
            {
                nomhoja = nombreHoja;
                nomhojaLista = nombrehojaLista;
                nrows = iniciafila;
                nrowsIni = IniciaFilaDetalle;
            }
            string url = Path.Combine(Environment.CurrentDirectory + "\\" + rutaArchivo, modelplantilla);

            var memoryStream = new MemoryStream();

            if (hssfwb == null)
            {
                using (FileStream file = new FileStream(url, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new XSSFWorkbook(file);
                }
            }

            ISheet excelSheet = hssfwb.GetSheet(nomhoja);
            IRow row = null;


            var font = hssfwb.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "Calibri";

            var fontSalesGp = hssfwb.CreateFont();
            fontSalesGp.FontHeightInPoints = 11;
            fontSalesGp.FontName = "Calibri";
            fontSalesGp.Color = IndexedColors.White.Index;

            var fontPorcentaje = hssfwb.CreateFont();
            fontSalesGp.FontHeightInPoints = 11;
            fontSalesGp.FontName = "Calibri";
            fontSalesGp.Color = IndexedColors.White.Index;

            //titulo
            ICellStyle tStyle = hssfwb.CreateCellStyle();
            tStyle.SetFont(font);


            //cabecera
            ICellStyle cStyle = hssfwb.CreateCellStyle();
            cStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
            cStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cStyle.FillPattern = FillPattern.SolidForeground;
            cStyle.Alignment = HorizontalAlignment.Center;
            cStyle.VerticalAlignment = VerticalAlignment.Center;
            cStyle.BorderBottom = BorderStyle.Medium;
            cStyle.BorderLeft = BorderStyle.Medium;
            cStyle.BorderRight = BorderStyle.Medium;
            cStyle.BorderTop = BorderStyle.Medium;
            cStyle.SetFont(font);

            //detalle izq
            ICellStyle diStyle = hssfwb.CreateCellStyle();
            diStyle.BorderBottom = BorderStyle.Thin;
            diStyle.BorderLeft = BorderStyle.Thin;
            diStyle.BorderRight = BorderStyle.Thin;
            diStyle.BorderTop = BorderStyle.Thin;

            //detalle der
            ICellStyle ddStyle = hssfwb.CreateCellStyle();
            ddStyle.BorderBottom = BorderStyle.Thin;
            ddStyle.BorderLeft = BorderStyle.Thin;
            ddStyle.BorderRight = BorderStyle.Thin;
            ddStyle.BorderTop = BorderStyle.Thin;


            if (nombreReporte == "")
            {
                row = excelSheet.CreateRow(3);
                ICell cell = row.CreateCell(nrowsIni + 2);
                cell.SetCellValue(nombreReporte + " " + param);
                cell.CellStyle = tStyle;
            }

            PropertyInfo[] pr = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] pr1 = typeof(X).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            row = excelSheet.GetRow(nrows);
            if (row == null) row = excelSheet.CreateRow(nrows);

            //genera cabecera
            if (nombreReporte == "")
            {
                foreach (PropertyInfo prop in pr)
                {

                    ICell cell = row.CreateCell(nrowsIni);
                    cell.SetCellValue(prop.Name.ToString());
                    cell.CellStyle = cStyle;
                    nrowsIni++;
                }

            }
            //genera cabecera
            int rowIniCabecera = 2;
            var _values = new object[pr1.Length];
            if (iniciaColumnaCabecera != 0)
            {
                for (int i = 0; i <= (pr1.Length - 1); i++)
                {
                    _values[i] = pr1[i].GetValue(_model, null);
                    row = excelSheet.GetRow(rowIniCabecera);
                    if (row == null) excelSheet.CreateRow(rowIniCabecera);
                    ICell cell = row.GetCell(iniciaColumnaCabecera);
                    if (cell == null) row.CreateCell(iniciaColumnaCabecera);
                    if (_values[i] != null) cell.SetCellValue(_values[i].ToString());
                    rowIniCabecera++;
                }

            }


            if (nomhojaLista != "")
            {
                excelSheet = hssfwb.GetSheet(nomhojaLista);
            }

            excelSheet = hssfwb.GetSheet(nomhoja);

            foreach (T item in model)
            {
                row = excelSheet.GetRow(nrows);
                if (row == null) row = excelSheet.CreateRow(nrows);
                var values = new object[pr.Length];
                for (int i = 0; i <= (pr.Length - 1); i++)
                {
                    values[i] = pr[i].GetValue(item, null);
                    var rowsBudget = row.RowNum;


                    if (values[i] != null)
                    {
                        ICell cell = row.GetCell(nrowsIni + i);

                        if (cell == null) cell = row.CreateCell(nrowsIni + i);

                        if (string.IsNullOrWhiteSpace(values[i].ToString()))
                        {
                            if (mostrarBordes == 1) cell.SetCellValue(String.Empty);
                            if (mostrarBordes == 1) cell.CellStyle = diStyle;


                        }
                        else
                        {

                            if (ParseString(values[i].ToString()) == "System.DateTime")
                            {
                                DateTime? vDate = Convert.ToDateTime(values[i].ToString());
                                short dateFormat = 0;
                                if (vDate != null && vDate?.Hour > 0)
                                    dateFormat = hssfwb.CreateDataFormat().GetFormat("dd/MM/yyyy HH:mm:ss");
                                else
                                    dateFormat = hssfwb.CreateDataFormat().GetFormat("dd/MM/yyyy");

                                ddStyle.Alignment = HorizontalAlignment.Left;

                                cell.SetCellValue((values[i].ToString()));
                                if (mostrarBordes == 1) cell.CellStyle = ddStyle;

                                cell.CellStyle.DataFormat = dateFormat;

                            }
                            else if (ParseString(values[i]) == "System.Int32" || ParseString(values[i]) == "System.Int64")
                            {
                                short dateFormat = hssfwb.CreateDataFormat().GetFormat("###,##0");
                                ddStyle.Alignment = HorizontalAlignment.Right;
                                cell.CellStyle.DataFormat = dateFormat;
                                cell.SetCellValue(Convert.ToInt64(values[i].ToString()));
                                if (mostrarBordes == 1) { cell.CellStyle = ddStyle; cell.CellStyle.DataFormat = dateFormat; }
                                if (mostrarBordes == 0) cell.CellStyle.DataFormat = dateFormat;

                            }

                            else if (ParseString(values[i]) == "System.Decimal")
                            {
                                // Obtener el formato de datos para valores numéricos Decimales
                                short dateFormat = hssfwb.CreateDataFormat().GetFormat("###,###,##0.00");

                                // Establecer la alineación de la celda a la derecha
                                ddStyle.Alignment = HorizontalAlignment.Right;

                                //    cell.CellStyle.DataFormat = dateFormat;

                                // Mostrar Celdas con Borde Negro si esta en cero no afectara el formato de celda .
                                if (mostrarBordes == 1) { cell.CellStyle = ddStyle; cell.CellStyle.DataFormat = dateFormat; }
                                if (mostrarBordes == 0) { cell.CellStyle = ddStyle; cell.CellStyle.DataFormat = dateFormat; }

                                // Establecer el valor de la celda como un valor numérico
                                cell.SetCellValue(Convert.ToDouble(values[i].ToString()));

                            }
                            else
                            {
                                diStyle.Alignment = HorizontalAlignment.Left;
                                cell.SetCellValue(values[i].ToString());

                                if (mostrarBordes == 1) cell.CellStyle = diStyle;
                                if (mostrarBordes == 4) cell.CellStyle = diStyle;


                            }
                        }

                    }
                }
                nrows++;
            }


            hssfwb.Write(memoryStream, false);

            return hssfwb;
        }

        public static async Task<string> SubirArchivoFirebase(string filePath, string fileName, string bucketName, string carpetaFirebase)
        {
            try
            {


                string filepath = Path.Combine(filePath, fileName);
                var url = "";

                if (File.Exists(filepath))
                {
                    using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                    {

                        var cancellation = new CancellationTokenSource();

                        var task = new FirebaseStorage(
                            bucketName,
                            new FirebaseStorageOptions
                            {
                                ThrowOnCancel = true,
                            })
                            .Child(carpetaFirebase)
                            .Child(fileName)
                            .PutAsync(stream);

                        url = await task;
                    }
                }

                return url;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static List<ListCbDTO> GetTimeZone(int start, string name_zone, int value_hour_mx)
        {
            List<ListCbDTO> hoursRange = new List<ListCbDTO>();

            for (var i = 0; i < 14; i++)
            {
                var inicio = start + i;
                var inicio_mx = value_hour_mx + i;


                start = inicio == 23 ? -1 - i : start;
                value_hour_mx = inicio_mx == 23 ? -1 - i : value_hour_mx;


                var formatted_inicio = inicio < 10 ? "0" + inicio : inicio.ToString();
                var formatted_inicio_mx = inicio_mx < 10 ? "0" + inicio_mx : inicio_mx.ToString();

                ListCbDTO item = new ListCbDTO
                {
                    id = i + 1,
                    vvalue1 = $"{formatted_inicio}:00 | HORA {name_zone}",
                    vvalue2 = $"{formatted_inicio}:00",
                    vvalue3 = $"{formatted_inicio_mx}:00",
                };

                hoursRange.Add(item);
            }
            return hoursRange;
        }





        public static IWorkbook CreateExcel2<X, T>(
            int IniciaFilaDetalle,  
            IWorkbook hssfwb, 
            string rutaArchivo, 
            string nombreHoja, 
            X cabeceraTabla, 
            List<T> model, 
            String nombreReporte, 
            string modelplantilla, 
            int iniciafila, 
            int iniciaColumnaCabecera, 
            string nombrehojaLista
            )
        {
            string fileName = nombreReporte + ".xlsx";
            string nomhoja = "";
            string nomhojaLista = "";
            int nrowsIni = 0;
            Int32 nrows = 0;

            nomhoja = "";
            nomhojaLista = "";


            if (nombreReporte != "")
            {
                nomhoja = nombreHoja;
                nomhojaLista = nombrehojaLista;
                nrows = iniciafila;
                nrowsIni = IniciaFilaDetalle;
            }

            string url = Path.Combine(Environment.CurrentDirectory + "\\" + rutaArchivo, modelplantilla);

            var memoryStream = new MemoryStream();

            if (hssfwb == null)
            {
                using (FileStream file = new FileStream(url, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new XSSFWorkbook(file);
                }
            }

            ISheet excelSheet = hssfwb.GetSheet(nomhoja);
            IRow row = null;

            #region ESTILOS

            var font = hssfwb.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "Calibri";

            var fontSalesGp = hssfwb.CreateFont();
            fontSalesGp.FontHeightInPoints = 11;
            fontSalesGp.FontName = "Calibri";
            fontSalesGp.Color = IndexedColors.White.Index;


            //titulo
            ICellStyle tStyle = hssfwb.CreateCellStyle();
            tStyle.SetFont(font);


            //cabecera
            ICellStyle cStyle = hssfwb.CreateCellStyle();
            cStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
            cStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cStyle.FillPattern = FillPattern.SolidForeground;
            cStyle.Alignment = HorizontalAlignment.Center;
            cStyle.VerticalAlignment = VerticalAlignment.Center;
            cStyle.BorderBottom = BorderStyle.Medium;
            cStyle.BorderLeft = BorderStyle.Medium;
            cStyle.BorderRight = BorderStyle.Medium;
            cStyle.BorderTop = BorderStyle.Medium;
            cStyle.SetFont(font);

            //detalle izq
            ICellStyle diStyle = hssfwb.CreateCellStyle();
            diStyle.BorderBottom = BorderStyle.Thin;
            diStyle.BorderLeft = BorderStyle.Thin;
            diStyle.BorderRight = BorderStyle.Thin;
            diStyle.BorderTop = BorderStyle.Thin;

            //detalle der
            ICellStyle ddStyle = hssfwb.CreateCellStyle();
            ddStyle.BorderBottom = BorderStyle.Thin;
            ddStyle.BorderLeft = BorderStyle.Thin;
            ddStyle.BorderRight = BorderStyle.Thin;
            ddStyle.BorderTop = BorderStyle.Thin;

            #endregion

            if (nombreReporte == "")
            {
                row = excelSheet.CreateRow(3);
                ICell cell = row.CreateCell(nrowsIni + 2);
                cell.SetCellValue(nombreReporte);
                cell.CellStyle = tStyle;
            }

            PropertyInfo[] pr = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] pr1 = typeof(X).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            row = excelSheet.GetRow(nrows);
            if (row == null) row = excelSheet.CreateRow(nrows);

            //genera cabecera
            if (nombreReporte == "")
            {
                foreach (PropertyInfo prop in pr)
                {

                    ICell cell = row.CreateCell(nrowsIni);
                    cell.SetCellValue(prop.Name.ToString());
                    cell.CellStyle = cStyle;
                    nrowsIni++;
                }

            }
            //genera cabecera
            int rowIniCabecera = 2;
            var _values = new object[pr1.Length];
            //if (iniciaColumnaCabecera != 0)
            //{
            //    for (int i = 0; i <= (pr1.Length - 1); i++)
            //    {
            //        _values[i] = pr1[i].GetValue(cabeceraTabla, null);
            //        row = excelSheet.GetRow(rowIniCabecera);
            //        if (row == null) excelSheet.CreateRow(rowIniCabecera);
            //        ICell cell = row.GetCell(iniciaColumnaCabecera);
            //        if (cell == null) row.CreateCell(iniciaColumnaCabecera);
            //        if (_values[i] != null) cell.SetCellValue(_values[i].ToString());
            //        rowIniCabecera++;
            //    }

            //}


            if (nomhojaLista != "")
            {
                excelSheet = hssfwb.GetSheet(nomhojaLista);
            }

            excelSheet = hssfwb.GetSheet(nomhoja);

            foreach (T item in model)
            {
                row = excelSheet.GetRow(nrows);
                if (row == null) row = excelSheet.CreateRow(nrows);
                var values = new object[pr.Length];
                for (int i = 0; i <= (pr.Length - 1); i++)
                {
                    values[i] = pr[i].GetValue(item, null);
                    var rowsBudget = row.RowNum;


                    if (values[i] != null)
                    {
                        ICell cell = row.GetCell(nrowsIni + i);

                        if (cell == null) cell = row.CreateCell(nrowsIni + i);

                        if (string.IsNullOrWhiteSpace(values[i].ToString()))
                        {
                            cell.SetCellValue(String.Empty);
                            cell.CellStyle = diStyle;


                        }
                        else
                        {

                            if (ParseString(values[i].ToString()) == "System.DateTime")
                            {
                                DateTime? vDate = Convert.ToDateTime(values[i].ToString());
                                short dateFormat = 0;
                                if (vDate != null && vDate?.Hour > 0)
                                    dateFormat = hssfwb.CreateDataFormat().GetFormat("dd/MM/yyyy HH:mm:ss");
                                else
                                    dateFormat = hssfwb.CreateDataFormat().GetFormat("dd/MM/yyyy");

                                ddStyle.Alignment = HorizontalAlignment.Left;

                                cell.SetCellValue((values[i].ToString()));
                                cell.CellStyle = ddStyle;

                                cell.CellStyle.DataFormat = dateFormat;

                            }
                            else if (ParseString(values[i]) == "System.Int32" || ParseString(values[i]) == "System.Int64")
                            {
                                short dateFormat = hssfwb.CreateDataFormat().GetFormat("###,##0");
                                ddStyle.Alignment = HorizontalAlignment.Right;
                                cell.CellStyle.DataFormat = dateFormat;
                                cell.SetCellValue(Convert.ToInt64(values[i].ToString()));
                                cell.CellStyle = ddStyle; 
                                cell.CellStyle.DataFormat = dateFormat; 

                            }

                            else if (ParseString(values[i]) == "System.Decimal")
                            {
                                // Obtener el formato de datos para valores numéricos Decimales
                                short dateFormat = hssfwb.CreateDataFormat().GetFormat("###,###,##0.00");

                                // Establecer la alineación de la celda a la derecha
                                ddStyle.Alignment = HorizontalAlignment.Right;

                                // Mostrar Celdas con Borde Negro si esta en cero no afectara el formato de celda .
                                cell.CellStyle = ddStyle; 
                                cell.CellStyle.DataFormat = dateFormat; 

                                // Establecer el valor de la celda como un valor numérico
                                cell.SetCellValue(Convert.ToDouble(values[i].ToString()));

                            }
                            else
                            {
                                diStyle.Alignment = HorizontalAlignment.Left;
                                cell.SetCellValue(values[i].ToString());

                                cell.CellStyle = diStyle;


                            }
                        }

                    }
                }
                nrows++;
            }


            hssfwb.Write(memoryStream, false);

            return hssfwb;
        }


    }
}
