﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IAdminsService {
        Task<bool> CrearMedico(UserRegistroDto nuevoMedico, String idUsuario);
    }
}