﻿namespace Neuromorfismo.BackEnd.Model;

public partial class EpilepsiaModel : BaseModel
{
    public int IdEpilepsia { get; set; }

    public string Nombre { get; set; } = null!;

    public ICollection<PacientesModel> Pacientes { get; set; } = new List<PacientesModel>();
}
