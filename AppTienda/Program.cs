// See https://aka.ms/new-console-template for more information
using BLL;
using ENTITIES.Entity;
using ENTITIES.DTO;
using System.Reflection.Emit;
using AppTienda.BLL;

Console.WriteLine("Ingrese el Usuario");
string Usuario = Console.ReadLine();

Console.WriteLine("Ingrese la contra");
string Contra = Console.ReadLine();

if(ValidaUsuario(Usuario,Contra))
{
    int Opc = 0;

    while (Opc != 5)
    {
        Console.WriteLine("=Sistema de Punto de Venta=");
        Console.WriteLine();
        Console.WriteLine("Bienvenido al sistema selecciona la opcion a realizar");
        Console.WriteLine("1. -Reporte de Inventario");
        Console.WriteLine("2. -Reporte de Venta");
        Console.WriteLine("3. -Venta");
        Console.WriteLine("4. -Agregar Producto");
        Console.WriteLine("5. - Salir");
        Console.WriteLine("Ingrese la opcion a realizar");
        Opc = Convert.ToInt32(Console.ReadLine());

        if (Opc == 1)
        {
            ReporteInventario();
        }
        else if (Opc == 2)
        {

        }
        else if (Opc == 3)
        {
            Venta();
        }
        else if (Opc == 4)
        {
            AgregarInventario();
        }
        else if (Opc == 5)
        {
            Console.WriteLine();
            Console.WriteLine("Saliendo del sistema..");
        }
    }

}
else
{
    Console.WriteLine("Acceso Denegado...");
    Console.WriteLine("Saliendo del sistema...");
}


Console.ReadKey();

static void AgregarInventario()
{
    int Opc = 0;
    INVENTARIO Inventario = new();

    Console.WriteLine("Ingrese la descripcion del inventario: ");
    Inventario.Descrip = Console.ReadLine();
    while(true)
    {
        Console.WriteLine("1.- Captura de SKU");
        Console.WriteLine("2.- Captura de Codigo de Barras");
        Console.WriteLine("Indique que valor desea capturar");
        Opc = Convert.ToInt32(Console.ReadLine());

        if (Opc == 1)
        {
            Console.Write("Ingrese el SKU: ");
            Inventario.SKU = Console.ReadLine();
            break;
        }
        else if (Opc == 2)
        {
            Console.WriteLine("Ingrese el codigo de barras: ");
            Inventario.CB = Console.ReadLine();
            break;
        }
        else
        {
            Console.WriteLine("Opcion no valida, ingrese una opcion: ");
            Console.WriteLine();
        }

    }

    Console.WriteLine("Ingrese el precio de venta");
    Inventario.PVenta = Convert.ToDecimal(Console.ReadLine());

    Console.WriteLine("Ingrese las existencias");
    Inventario.Existencias = Convert.ToInt32(Console.ReadLine());

    List<DtoCatCategoria> lstCategoria = BL_CATEGORIA.ListarCategoria();

    foreach(var lst in lstCategoria)
    {
        Console.WriteLine($"{lst.IdCategoria} - {lst.Categoria}");
    }
    Console.WriteLine("Selecciona una opcion");
    Inventario.IdCategoria = Convert.ToInt32(Console.ReadLine());


    Console.WriteLine("Ingrese el IVA seleccionado");
    Inventario.IVA = Convert.ToDecimal(Console.ReadLine());

    List<string> lstValidaciones = BL_INVENTARIO.ValidarProducto(Inventario);
    if (lstValidaciones.Count == 0)
    {
        List<string> lstRespuesta = BL_INVENTARIO.GuardarInventario(Inventario);
        if (lstRespuesta[0] == "00")
        {
            Console.WriteLine(lstRespuesta[1]);
        }
        else
        {
            Console.WriteLine(lstRespuesta[1]);
        }

    }
    else
    {
        Console.WriteLine($"revise el error {lstValidaciones[0]}");
    }


}
static void ReporteInventario()
{
    List<DtoRepInventario> lstRepInventario = BL_INVENTARIO.ReporteInventario();
    Console.WriteLine($"SKU | CB | Descrip | PVenta | Existencias | Categoria | IVA | Estatus");
    foreach (var lst in lstRepInventario)
    {
        Console.WriteLine($" {lst.SKU} | {lst.CB} | {lst.Descrip} | {lst.PVenta} | {lst.Existencias} | {lst.Categoria} | {lst.IVA} | {lst.Estatus}");
    }
}

static bool ValidaUsuario(string PUsuario, string PContra)
{
    bool Resultado = false;
    string Usuario = "admin", Contra = "admin";

    if (PUsuario == Usuario && PContra == Contra)
    {
        Resultado = true;
    }
    return Resultado;

}

static void Venta()
{
    List<DtoCarrito> lstCarrito = [];
    DtoCarrito InfoArticulo = new DtoCarrito();

    while (true)
    {
        DtoCarrito InfoArticulo = new DtoCarrito();
        Console.WriteLine("Ingrese el SKU / CB del articulo");
        string Articulo = Console.ReadLine();

        List<DtoCarrito> lstDatos = BL_INVENTARIO.ConsultaDatosArticulo(Articulo);

        if (lstDatos.Count > 0)
        {
            Console.WriteLine("Ingrese la cantidad a comprar");
            int Cantidad = Convert.ToInt32(Console.ReadLine());

            InfoArticulo.Descrip = lstDatos[0].Descrip;
            InfoArticulo.IdInventario = lstDatos[0].IdInventario;
            InfoArticulo.Cantidad = Cantidad;
            InfoArticulo.Precio = lstDatos[0].Precio;
            InfoArticulo.SubTotal = (lstDatos[0].Precio * Cantidad);
            InfoArticulo.Total = InfoArticulo.SubTotal * ((lstDatos[0].IVA / 100) + 1);
            InfoArticulo.IVA = lstDatos[0].IVA;

            lstDatos = null;
            lstCarrito.Add(InfoArticulo);


            Console.WriteLine("=Carrito=");
            Console.WriteLine();


            Console.WriteLine($"Articulo | Cantidad | Precio | SubTotal | Total");
            foreach (var lst in lstCarrito)
            {
                Console.WriteLine($"{lst.Descrip} | {lst.Cantidad} | {lst.Precio} |  {lst.SubTotal} | {lst.Total}");
            }

            Console.WriteLine($"SubTotal: {lstCarrito.Sum(lst => lst.SubTotal)}");
            Console.WriteLine($"Total: {lstCarrito.Sum(lst => lst.Total)}");

            Console.WriteLine("1.- Pagar");
            Console.WriteLine("2.- Seguir Agregando");
            Console.WriteLine("3.- Cancelar Venta");
            Console.WriteLine("Ingrese la opcion a realizar: ");
            int Opc = Convert.ToInt32(Console.ReadLine());

            if (Opc == 1)
            {
                while (true)
                {
                    Console.WriteLine("Ingrese el pago");
                    decimal Pago = Convert.ToDecimal(Console.ReadLine());
                    List<string> lstValidacion = BL_Venta.ValidacionVenta(lstCarrito.Sum(lst => lst.Total), Pago, lstCarrito);
                    if()
                }
            }
            else if (Opc == 2)
            {
                ReporteVenta();
            }
            else if (Opc == 3)
            {
                Console.WriteLine("");
                Console.WriteLine("Venta Cancelada..");
                break;
            }
            else 
            {
                Console.WriteLine("Ingrese una opcion disponible");
            }

        }
        else
        {
            Console.WriteLine("El Articulo No Fue Encontrado");
            Console.WriteLine();
        }

    } 
    
}

Console.Writeline("-------");
Console.WriteLine($"No. Ticket: {lstVenta.Ticket}");
Console.Writeline($"Total: {lstVenta.Total.ToString("c")} Pago: {lstVentaPago}");
