using System.ComponentModel;

namespace SaludMovil.Transversales
{
    public enum TiposIdentificacion
    {

        [DescriptionAttribute("Cédula de ciudadania")]
        CC = 0,
        [DescriptionAttribute("NIT")]
        NIT=1,
        [DescriptionAttribute("Cédula de extranjería")]
        CE=2,
        [DescriptionAttribute("")]
        TI=3,
        [DescriptionAttribute("")]
        RC=4
    }
    public enum Generos
    {
        [DescriptionAttribute("Masculino")]
        M = 0,
        [DescriptionAttribute("Femenino")]
        F = 1,
    }

    public enum Sino
    {
        [DescriptionAttribute("Si")]
        Si = 1,
        [DescriptionAttribute("No")]
        No = 0,
    }

    public enum TipoGuiaEnum
    {
        [DescriptionAttribute("Activo")]
        Activo = 1,
        [DescriptionAttribute("Inactivo")]
        Inactivo = 0,
    }
}