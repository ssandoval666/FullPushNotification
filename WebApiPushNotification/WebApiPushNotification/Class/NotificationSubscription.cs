using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPushNotification.Class
{
    ///<Summary>
    /// Clase para el Manejo de Subscripciones
    ///</Summary>
    public class NotificationSubscription
    {
        ///<Summary>
        /// Url donde se mandaran los push
        ///</Summary>
        public string Url { get; set; }

        ///<Summary>
        /// Key
        ///</Summary>
        public string P256dh { get; set; }

        ///<Summary>
        /// Autorizacion
        ///</Summary>
        public string Auth { get; set; }
    }

    ///<Summary>
    /// Clase para el envio de mensajes
    ///</Summary>
    ///
    public class NotificationMessageText
    {
        ///<Summary>
        /// Mensaje de la notificacion
        ///</Summary>
        public string message { get; set; }

        ///<Summary>
        /// Cuerpo de la notificacion
        ///</Summary>
        public string body { get; set; }

        ///<Summary>
        /// Acciones de la notificacion
        ///</Summary>
        public NotifationAction[] actions { get; set; }

        ///<Summary>
        /// Indica si la notificacion requiere interaccion
        ///</Summary>
        public bool requireInteraction { get; set; } = true;

        ///<Summary>
        /// Para el tipo de vibracion
        ///</Summary>
        public int[] vibrate { get; set; } = new int[] { 100, 50, 100 };
    }

    ///<Summary>
    /// Clase de Acciones
    ///</Summary>
    public class NotifationAction
    {
        ///<Summary>
        /// Tipo de accion
        ///</Summary>
        public string action { get; set; }

        ///<Summary>
        /// Descripcion
        ///</Summary>
        public string title { get; set; }

        ///<Summary>
        /// Icono de la descripcion
        ///</Summary>
        public string icon { get; set; }
    }

   
}