using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Crud" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Crud.svc or Crud.svc.cs at the Solution Explorer and start debugging.
    public class Crud : ICrud
    {
        public void DoWork()
        {
        }

        public SL_WCF.Result Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateEF(usuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result GetById(int idUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(idUsuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects,
            };
        }
    }
}
