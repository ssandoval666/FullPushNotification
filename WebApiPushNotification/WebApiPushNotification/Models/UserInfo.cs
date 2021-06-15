using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPushNotification.Models
{
    ///<Summary>
    /// Clase de Datos de Acceso
    ///</Summary>
    ///
    public partial class UserInfo
    {
        ///<Summary>
        /// Id de usuario
        ///</Summary>
        ///
        public int UserId { get; set; }

        ///<Summary>
        /// Nombre
        ///</Summary>
        ///
        public string FirstName { get; set; }

        ///<Summary>
        /// Apellido
        ///</Summary>
        ///
        public string LastName { get; set; }

        ///<Summary>
        /// Nombre de Usuario
        ///</Summary>
        ///
        public string UserName { get; set; }

        ///<Summary>
        /// Correo Electronico
        ///</Summary>
        ///
        public string Email { get; set; }

        ///<Summary>
        /// Contraseña
        ///</Summary>
        ///
        public string Password { get; set; }

        ///<Summary>
        /// Fecha de Creacion
        ///</Summary>
        ///
        public DateTime CreatedDate { get; set; }
    }
}
