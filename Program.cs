int ej;

do {
    Console.Clear();
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

    switch (ej) {
        case 1: Ejercicio1(); break;
        case 2: Ejercicio2(); break;
        case 3: Ejercicio3(); break;
        case 4: Ejercicio4(); break;
        case 5: Ejercicio5(); break;
        case 0: Console.WriteLine("Saliendo del programa..."); break;
        default: Console.WriteLine("Opción no válida. Inténtalo de nuevo."); break;
    }

    if (ej != 0) {
        Console.WriteLine();
        Console.Write("Pulsa ENTER para volver al menú...");
        Console.ReadLine();
    }

} while (ej != 0);


void Ejercicio1() {
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 1 ---");
    Console.WriteLine();

    var registro = new RegistroVentas();
    var servicioRegistro = new ServicioRegistro();
    var servicioNotificacion = new ServicioNotificacion();

    registro.VentaRealizada += servicioRegistro.RegistrarVenta;
    registro.VentaRealizada += servicioNotificacion.EnviarNotificacionVenta;
    
    Console.Write("Nombre del Primer Producto: ");
    string nameProduc1 = Console.ReadLine();
    Console.WriteLine();

    Console.Write("Precio del Primer Producto: ");
    decimal precioProduc1 = Convert.ToDecimal(Console.ReadLine());
    Console.WriteLine();

    registro.ProcesarVenta(nameProduc1, precioProduc1);
    Console.WriteLine();

    Console.Write("Nombre del Segundo Producto: ");
    string nameProduc2 = Console.ReadLine();
    Console.WriteLine();

    Console.Write("Precio del Primer Producto: ");
    decimal precioProduc2 = Convert.ToDecimal(Console.ReadLine());
      
    Console.WriteLine();
    registro.ProcesarVenta(nameProduc2, precioProduc2);
}

void Ejercicio2() {
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 2 ---");
    Console.WriteLine();

    Console.Write("Introduce la temperatura maxima permitida: ");
    int max = Convert.ToInt32(Console.ReadLine());

    var control = new ControlTemperatura(max);
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

void Ejercicio3() {
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 3 ---");
    Console.WriteLine();

    var gestor = new GestorBackups();
    var notificacion = new ServicioNotificacionBackup();
    var log = new ServicioLog();

    gestor.BackupCompletado += notificacion.EnviarNotificacion;
    gestor.BackupCompletado += log.RegistrarOperacion;

    Console.Write("Nombre del Primer Archivo: ");
    string nameDocument1 = Console.ReadLine();

    Console.WriteLine();
    gestor.RealizarBackup(nameDocument1);
    Console.WriteLine();

    Console.Write("Nombre del Segundo Archivo: ");
    string nameDocument2 = Console.ReadLine();

    Console.WriteLine();
    gestor.RealizarBackup(nameDocument2);
}

void Ejercicio4() {
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 4 ---");

    var sensor = new SensorMonitoreo(new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));
    var alarma = new ServicioAlarma();
    var incidencias = new ServicioRegistroIncidencias();

    sensor.AlertaIntruso += alarma.ActivarAlarma;
    sensor.AlertaIntruso += incidencias.GuardarRegistro;

    Console.WriteLine();
    sensor.DetectarApertura("Puerta principal");
    Console.WriteLine();
    sensor.DetectarApertura("Ventana cocina");
}

void Ejercicio5() {
    Console.WriteLine();
    Console.WriteLine("--- Ejercicio 5 ---");
    Console.WriteLine();

    Console.Write("Introduce el límite de consumo permitido (kWh): ");
    double limite = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine();

    var supervisor = new SupervisorConsumo(limite);
    var notificacion = new ServicioNotificacionConsumo();
    var ajuste = new ServicioAjusteAutomatizado();

    supervisor.ConsumoExcesivo += notificacion.EnviarAdvertencia;
    supervisor.ConsumoExcesivo += ajuste.AjustarDispositivos;

    Console.Write("Introduce el consumo actual del primer dispositivo (kWh): ");
    double consumo1 = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Introduce el consumo actual del segundo dispositivo (kWh): ");
    double consumo2 = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine();

    double consumoTotal = consumo1 + consumo2;

    supervisor.SupervisarConsumo(consumoTotal);
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
    public void EnviarAlerta(object sender, TemperaturaEventArgs e) {
        Console.WriteLine($"[ALERTA] ¡Temperatura alta! {e.TemperaturaActual}°C supera el umbral de {e.Umbral}°C");
    }
}

public class ServicioRegistroTemperatura {
    public void RegistrarTemperatura(object sender, TemperaturaEventArgs e) {
        Console.WriteLine($"[REGISTRO] Temperatura registrada: {e.TemperaturaActual}°C");
    }
}

// Sistema de Backup y Notificación de Archivos
public class BackupEventArgs : EventArgs {
    public string NombreArchivo { get; set; }
    public DateTime Fecha { get; set; }

    public BackupEventArgs(string nombreArchivo, DateTime fecha) {
        NombreArchivo = nombreArchivo;
        Fecha = fecha;
    }
}

public class GestorBackups {
    public event EventHandler<BackupEventArgs> BackupCompletado;

    public void RealizarBackup(string nombreArchivo) {
        Console.WriteLine($"Realizando copia de seguridad de '{nombreArchivo}'...");
        OnBackupCompletado(new BackupEventArgs(nombreArchivo, DateTime.Now));
    }

    protected virtual void OnBackupCompletado(BackupEventArgs e) {
        BackupCompletado?.Invoke(this, e);
    }
}

public class ServicioNotificacionBackup {
    public void EnviarNotificacion(object sender, BackupEventArgs e) {
        Console.WriteLine($"[NOTIFICACIÓN] Backup completado de '{e.NombreArchivo}' a las {e.Fecha:HH:mm:ss}");
    }
}

public class ServicioLog
{
    public void RegistrarOperacion(object sender, BackupEventArgs e) {
        Console.WriteLine($"[LOG] Operación registrada: {e.NombreArchivo} - {e.Fecha}");
    }
}

// Sistema de Monitoreo de Sensores de Puertas y Ventanas
public class IntrusoEventArgs : EventArgs
{
    public string NombreSensor { get; set; }
    public DateTime HoraDeteccion { get; set; }

    public IntrusoEventArgs(string nombreSensor, DateTime horaDeteccion)
    {
        NombreSensor = nombreSensor;
        HoraDeteccion = horaDeteccion;
    }
}

public class SensorMonitoreo
{
    private readonly TimeSpan _horaInicioPermitida;
    private readonly TimeSpan _horaFinPermitida;

    public event EventHandler<IntrusoEventArgs> AlertaIntruso;

    public SensorMonitoreo(TimeSpan horaInicioPermitida, TimeSpan horaFinPermitida) {
        _horaInicioPermitida = horaInicioPermitida;
        _horaFinPermitida = horaFinPermitida;
    }

    public void DetectarApertura(string nombreSensor) {
        var horaActual = DateTime.Now.TimeOfDay;
        bool fueraDeHorario = horaActual < _horaInicioPermitida || horaActual > _horaFinPermitida;

        Console.WriteLine($"Sensor '{nombreSensor}' abierto a las {DateTime.Now:HH:mm:ss}");
        if (fueraDeHorario)
        {
            OnAlertaIntruso(new IntrusoEventArgs(nombreSensor, DateTime.Now));
        }
        else
        {
            Console.WriteLine("(dentro del horario permitido, sin alerta)");
        }
    }

    protected virtual void OnAlertaIntruso(IntrusoEventArgs e) {
        AlertaIntruso?.Invoke(this, e);
    }
}

public class ServicioAlarma {
    public void ActivarAlarma(object sender, IntrusoEventArgs e) {
        Console.WriteLine($"[ALARMA] ¡INTRUSO DETECTADO! Sensor: {e.NombreSensor} a las {e.HoraDeteccion:HH:mm:ss}");
    }
}

public class ServicioRegistroIncidencias {
    public void GuardarRegistro(object sender, IntrusoEventArgs e) {
        Console.WriteLine($"[BD] Incidencia guardada: {e.NombreSensor} - {e.HoraDeteccion}");
    }
}

// Sistema de Supervisión de Consumo de Energía
public class ConsumoEventArgs : EventArgs {
    public double ConsumoActual { get; set; }
    public double LimiteConsumo { get; set; }

    public ConsumoEventArgs(double consumoActual, double limiteConsumo) {
        ConsumoActual = consumoActual;
        LimiteConsumo = limiteConsumo;
    }
}

public class SupervisorConsumo {
    private readonly double _limiteConsumo;

    public event EventHandler<ConsumoEventArgs> ConsumoExcesivo;

    public SupervisorConsumo(double limiteConsumo) {
        _limiteConsumo = limiteConsumo;
    }

    public void SupervisarConsumo(double consumoActual) {
        Console.WriteLine($"Consumo actual: {consumoActual} kWh (límite permitido: {_limiteConsumo} kWh)");
        if (consumoActual > _limiteConsumo) {
            OnConsumoExcesivo(new ConsumoEventArgs(consumoActual, _limiteConsumo));
        }
    }

    protected virtual void OnConsumoExcesivo(ConsumoEventArgs e) {
        ConsumoExcesivo?.Invoke(this, e);
    }
}

public class ServicioNotificacionConsumo {
    public void EnviarAdvertencia(object sender, ConsumoEventArgs e) {
        Console.WriteLine($"[ADVERTENCIA] El consumo ({e.ConsumoActual} kWh) supera el límite ({e.LimiteConsumo} kWh).");
    }
}

public class ServicioAjusteAutomatizado {
    public void AjustarDispositivos(object sender, ConsumoEventArgs e) {
        Console.WriteLine("[AJUSTE] Reduciendo consumo automáticamente de los dispositivos...");
    }
}