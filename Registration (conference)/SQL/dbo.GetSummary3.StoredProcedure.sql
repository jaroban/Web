SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSummary3]
AS
BEGIN
    declare @piatokVeceraZaplatene int;
    declare @piatokVecera2Zaplatene int;
    declare @sobotaRanajkyZaplatene int;
    declare @sobotaObedZaplatene int;
    declare @sobotaVeceraZaplatene int;
    declare @sobotaVecera2Zaplatene int;
    declare @nedelaRanajkyZaplatene int;
    declare @nedelaObedZaplatene int;
    
    -- zaplatene
    select
        @piatokVeceraZaplatene = sum(cast([PiatokVecera] as int)),
        @piatokVecera2Zaplatene = sum([PiatokVecera2]),
        @sobotaRanajkyZaplatene = sum(cast([SobotaRanajky] as int)),
        @sobotaObedZaplatene = sum(cast([SobotaObed] as int)),
        @sobotaVeceraZaplatene = sum(cast([SobotaVecera] as int)),
        @sobotaVecera2Zaplatene = sum([SobotaVecera2]),
        @nedelaRanajkyZaplatene = sum(cast([NedelaRanajky] as int)),
        @nedelaObedZaplatene = sum(cast([NedelaObed] as int))
    from [dbo].[registracia] r
    where 
        [DtPlatba] is not null or
        [idZbor] = 71 or
        [IstoPride] = 1 or
        [RegistraciaZadarmo] = 1 or
        [JedloZadarmo] = 1;

    -- counts
    select  count(*),
            sum(cast([PiatokVecera] as int)), @piatokVeceraZaplatene,
            sum([PiatokVecera2]), @piatokVecera2Zaplatene,
            sum(cast([UbytovaniePiatokSobota] as int)),
            sum(cast([TichaTriedaPiatokSobota] as int)),
            sum(cast([SobotaRanajky] as int)), @sobotaRanajkyZaplatene,
            sum(cast([SobotaObed] as int)), @sobotaObedZaplatene,
            sum(cast([SobotaVecera] as int)), @sobotaVeceraZaplatene,
            sum([SobotaVecera2]), @sobotaVecera2Zaplatene,
            sum(cast([UbytovanieSobotaNedela] as int)),
            sum(cast([TichaTriedaSobotaNedela] as int)),
            sum(cast([NedelaRanajky] as int)), @nedelaRanajkyZaplatene,
            sum(cast([NedelaObed] as int)), @nedelaObedZaplatene,
            sum(cast([Sach] as int)),
            sum(cast([PingPong] as int)),
            sum(case when [Sluziaci] = 'Dobrovolnik' then 1 else 0 end),
            sum(cast([Internat1] as int)),
            sum(cast([Internat2] as int))
    from [dbo].[registracia];
    
    -- t-shirts
    select t.[name], count(*)
    from [dbo].[registracia] r
    left join [dbo].[tricko] t on r.[IdTricko] = t.[id]
    where r.[IdTricko] is not null
    group by t.[name];
    
    -- t-shirts paid
    select t.[name], count(*)
    from [dbo].[registracia] r
    left join [dbo].[tricko] t on r.[IdTricko] = t.[id]
    where 
        r.[IdTricko] is not null and
        ([DtPlatba] is not null or
        [idZbor] = 71 or
        [IstoPride] = 1 or
        [RegistraciaZadarmo] = 1)
    group by t.[name];
    
    -- expecting euros
    select sum([amount])
    from [dbo].[receivable]
    where [idCurrency] = 1;
    
    -- expecting czk
    select sum([amount])
    from [dbo].[receivable]
    where [idCurrency] = 2;
    
    -- paid by people
    select SUM(case when [idCurrency] = 2 then [amount] / 27.61 else [amount] end)
    from [dbo].[payment];
    
    -- donations from churches
    select SUM([amount])
    from [dbo].[import];

    -- sluziaci
    select [Meno], [Priezvisko], [Email], [Telefon], [Poznamka]
    from [dbo].[registracia]
    where [Sluziaci] = 'Dobrovolnik';
    
    -- poznamka
    select [Meno], [Priezvisko], [Email], [Telefon], [Poznamka]
    from [dbo].[registracia]
    where [Poznamka] <> '' and [Sluziaci] <> 'Dobrovolnik';
    
    -- financna pomoc
    select [Meno], [Priezvisko], rg.[Email], [Telefon], [Poznamka]
    from [dbo].[registracia] rg
    left join [dbo].[receivable] rx on rx.[id] = rg.[idVariabilny]
    where rx.[needHelp] = 1;
    
    -- by church
    select coalesce(z.[name], r.[InyZbor]) as church, COUNT(*) as count
    from [dbo].[registracia] r
    left join [dbo].[zbor] z on z.id = r.IdZbor
    group by coalesce(z.[name], r.[InyZbor])
    order by count desc, church asc;
END
GO
