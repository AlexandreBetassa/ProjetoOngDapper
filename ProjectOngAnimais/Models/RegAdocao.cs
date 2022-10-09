using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RegAdocao
    {
        public static readonly string INSERT = "INSERT INTO dbo.regAdocao (cpf, NChip, data) VALUES (@cpf, @NChip, @data)";
        public static readonly string SELECT = "SELECT pessoa.nome, animal.NChip, regAdocao.data " +
                                        "FROM regAdocao, pessoa, animal " +
                                        "WHERE pessoa.cpf = regAdocao.cpf AND animal.NCchip = regAdocao.NChip";
        public String Cpf { get; set; }
        public int NChip { get; set; }
        public DateTime Data { get; set; }

    }
}
