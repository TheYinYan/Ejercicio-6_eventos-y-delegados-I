int ej;

do
{
    Console.WriteLine();
    Console.WriteLine("1. Sistema de Registro y Notificación de Ventas");
    Console.WriteLine("2. Sistema de Control de Temperatura en un Invernadero");
    Console.WriteLine("3. Sistema de Backup y Notificación de Archivos");
    Console.WriteLine("4. Sistema de Monitoreo de Sensores de Puertas y Ventanas");
    Console.WriteLine("5. Sistema de Supervisión de Consumo de Energía");
    Console.WriteLine("0. Salir");
    Console.WriteLine();
    Console.Write("Elige una opción: ");
    ej = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    switch (ej)
    {
        case 1: Ejercicio1(); break;
        case 2: Ejercicio2(); break;
        case 3: Ejercicio3(); break;
        case 4: Ejercicio4(); break;
        case 5: Ejercicio5(); break;
        case 0: Console.WriteLine("Saliendo del programa..."); break;
        default: Console.WriteLine("Opción no válida. Inténtalo de nuevo."); break;
    }

} while (ej != 0);


void Ejercicio1(){
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 1 ---");
    Console.WriteLine();

    var registro = new RegistroVentas();
    var servicioRegistro = new ServicioRegistro();
    var servicioNotificacion = new ServicioNotificacion();

    Console.WriteLine();
    registro.VentaRealizada += servicioRegistro.RegistrarVenta;
    Console.WriteLine();
    registro.VentaRealizada += servicioNotificacion.EnviarNotificacionVenta;

    Console.WriteLine();
    registro.ProcesarVenta("Laptop", 1200.50m);     
    Console.WriteLine();
    registro.ProcesarVenta("Mouse", 25.99m);
}

void Ejercicio2() {
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 2 ---");
    Console.WriteLine();

    var control = new ControlTemperatura(30);
    var alerta = new ServicioAlerta();
    var registroTemp = new ServicioRegistroTemperatura();

    control.TemperaturaAlta += alerta.EnviarAlerta;
    control.TemperaturaAlta += registroTemp.RegistrarTemperatura;

    Console.WriteLine();
    control.SupervisarTemperatura(25);
    Console.WriteLine();
    control.SupervisarTemperatura(32);
    Console.WriteLine();
    control.SupervisarTemperatura(35);

}

void Ejercicio3()
{
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 3 ---");
    Console.WriteLine();
}

void Ejercicio4()
{
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 4 ---");
    Console.WriteLine();
}

void Ejercicio5()
{
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 5 ---");
    Console.WriteLine();
}

//  Sistema de Registro y Notificación de Ventas
public class VentaEventArgs : EventArgs {
    public string Producto { get; set; }
    public decimal Precio { get; set; }

    public VentaEventArgs(string producto, decimal precio)
    {
        Producto = producto;
        Precio = precio;
    }
}
public class RegistroVentas {
    public event EventHandler<VentaEventArgs> VentaRealizada;
    public void ProcesarVenta(string producto, decimal precio)
    { 
        Console.WriteLine($"Procesando venta de '{producto}' por ${precio}...");
        OnVentaRealizada(new VentaEventArgs(producto, precio));
    }
    protected virtual void OnVentaRealizada(VentaEventArgs e)
    {
        VentaRealizada?.Invoke(this, e);
    }
}
public class ServicioRegistro {
    public void RegistrarVenta(object sender, VentaEventArgs e)
    {
        Console.WriteLine($"[REGISTRO] Venta guardada: {e.Producto} - ${e.Precio}");
    }
}

public class ServicioNotificacion
{
    public void EnviarNotificacionVenta(object sender, VentaEventArgs e)
    {
        Console.WriteLine($"[NOTIFICACIÓN] Se ha realizado una venta de {e.Producto} por ${e.Precio}");
    }
}

// Sistema de Control de Temperatura en un Invernadero
public class TemperaturaEventArgs : EventArgs {
    public double TemperaturaActual { get; set; }
    public double Umbral { get; set; }

    public TemperaturaEventArgs(double temperaturaActual, double umbral)
    {
        TemperaturaActual = temperaturaActual;
        Umbral = umbral;
    }
}

public class ControlTemperatura{
    private readonly double _umbral;
    public event EventHandler<TemperaturaEventArgs> TemperaturaAlta;
    public ControlTemperatura(double umbral) {
        _umbral = umbral;
    }
    public void SupervisarTemperatura(double temperatura) {
        Console.WriteLine($"Temperatura actual: {temperatura}°C (umbral: {_umbral}°C)");
        if (temperatura > _umbral) {
            OnTemperaturaAlta(new TemperaturaEventArgs(temperatura, _umbral));
        }
    }
    protected virtual void OnTemperaturaAlta(TemperaturaEventArgs e) {
        TemperaturaAlta?.Invoke(this, e);
    }
}

public class ServicioAlerta {
    public void EnviarAlerta(object sender, TemperaturaEventArgs e)
    {
        Console.WriteLine($"[ALERTA] ¡Temperatura alta! {e.TemperaturaActual}°C supera el umbral de {e.Umbral}°C");
    }
}

public class ServicioRegistroTemperatura {
    public void RegistrarTemperatura(object sender, TemperaturaEventArgs e)
    {
        Console.WriteLine($"[REGISTRO] Temperatura registrada: {e.TemperaturaActual}°C");
    }
}