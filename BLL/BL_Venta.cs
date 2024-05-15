using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppTienda.BLL
{
    public class BL_Venta
    {
        public static List<string> ValidacionVenta(decimal PMontoPago, decimal PPago,List<DtoCarrito> PlstCarrito) 
        {
            List<string> lstValidacion = [];
            if ( PPago < PMontoPago) 
            {
                lstValidacion.Add("El Pago debe ser mayor al monto a pagar");
            }
            foreach (var lst in PlstCarrito) 
            {
                if (ValidaExistenciasArticulo(lst.IdInventario, lst.Cantidad)) 
                {
                    lstValidacion.Add($"Articulo {lst.Descrip} no cuenta con existencias para su venta");
                }
            }

            return lstValidacion;
        }
        private static bool ValidaExistenciasArticulo(int PIdInventario)
        {
            bool Validacion = false;
            string SQLScript = "SELECT 1 AS Resultado\r\nFROM INVENTARIO\r\nWHERE IdInventario = @P_Cantidad AND\r\nEXISTENCIAS >= @P_Cantidad"


                var dpParemetros = new
                {
                    P_IdInventarios = PIdInventario,
                    P_Cantidad = Pcantidad
                };
                
            return Validacion;
        }
    }
}

string SQLScript = "SELECT  VEN.IdVenta AS Ticket,\r\n        VDE.Total,\r\n        VEN,MontoPagado as Pago,\r\n        VEN.Feria as Cambio,\r\n        FORMAT(VEN.FECVenta, 'dd/MM/yyyy HH:mm:ss') as FechaVenta\r\nFROM VENTA AS VEN\r\n        INNER JOIN (SELECT IdVenta,\r\n                            SUM(((PVenta *(1+(IVA/100)))*Cantidad)) as Total\r\n        FROM VENTA_DETALLE\r\n        GROUP BY IdVenta AS VDE ON VEN.IdVenta = VDE.IdVenta";

var dpParametros = new { };

DataTable dt = Contexto.Funcion_ScriptDB(Conn, SQLScript, dpParametros);

if (dt.Rows.Count > 0) 
{
    lstVentas = [..
        dt.AsEnumerable().Select(item =>)


        ]
}

foreach (var lst in lstVentas) 
{
    lst.VentaDetalle = ReporteVentaVentaDetalle(lst.Ticket);
}

return lstVentas;

private static List<DtoRepVentaDetalle> ReporteVentaDetalle(int PIdVenta) 
{
    List<DtoRepVentaDetalle> lstVentaDetalle = [];
    string SQLScript = " SELECT  INV.Articulo\r\n        Cantidad,\r\n        PVenta,\r\n        IVA,\r\n        ((PVenta *(1+(IVA/100)))*Cantidad) as Total\r\nFROM VENTA_DETALLE AS VDE\r\n        INNER JOIN (SELECT IdInventario,\r\n                            UPPER(Descrip) AS Articulo\r\n        FROM INVENTARIO AS INV ON VDE.IdInventario = INV.IdInventario\r\nWHERE IdVenta = @P_IdVenta";

    var dpParametros = new
    {
        P_IdVenta = PIdVenta
    };

    DataTable Dt = Contexto.Funcion_ScriptDB(Conn, SQLScript, dpParametros);
    if (Dt.Rows.Count > 0)
    {
        lstVentaDetalle = [
            .. Dt.AsEnumerable().Select(item => new DtoRepVentaDetalle)
            {
                Articulo = Item.Field<string>("Articulo"),
                Articulo = Item.Field<string>("Articulo"),
                Articulo = Item.Field<string>("Articulo"),
                Articulo = Item.Field<string>("Articulo"),
                Articulo = Item.Field<string>("Articulo")
            })];
    }
}