## Introducción
¡Bienvenido! 🚀
La Aplicación, tiene como finalidad consumir los archivos *.fct guardados en la carpeta de Pendientes. Los archivos, se limpiarán de los caracteres no deseados y se seccionarán en los campos correspondientes para intentar agregar el ticket a nuestra tabla Tickets, alojada en la base de datos mediante un Stored Procedure. Posteriormente, a través de un Trigger, se alimentará la tabla de Resumen para ofrecer un histórico que contiene el Id de la Tienda, Id de la Registradora y el Total de Tickets con esta combinación. Sea correcta o no la inserción, los archivos se moverán de la carpeta a Procesados. Si la inserción no fue correcta, su extensión cambiará a *.fct_error.

He expuesto la lógica de negocio a través de una REST API, utilizando ASP.NET Web API. Se probó en POSTMAN y posteriormente fue consumida desde un Servicio Windows.
## Manual de Usuario📘
Puedes encontrar el manual completo en formato PDF [aquí](https://github.com/LuMeza/evaluacion_gMeza/blob/master/Manual/ManualEvaluacionGMeza.pdf).📄
