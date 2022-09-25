select * from pessoa

select * from pet

select * from regAdocao

delete from regAdocao where nChipPet = 1

select p.cpf, p.nome, pt.nChipPet, pt.familiaPet, pt.racaPet
from pessoa p, pet pt, regAdocao ra
where ra.cpf = p.cpf and pt.nChipPet = ra.nChipPet;