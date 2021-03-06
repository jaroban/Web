SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateOrAddUser]
    @idUser                     [int] = null,
    @Meno                       [nvarchar](max),
    @Priezvisko                 [nvarchar](max),
    @Email                      [nvarchar](max),
    @Telefon                    [nvarchar](max),
    @IdZbor                     [int] = null,
    @InyZbor                    [nvarchar](max),
    
    @PiatokVecera               [bit],
    @PiatokVecera2              [bit],
    @UbytovaniePiatokSobota     [bit],
    @TichaTriedaPiatokSobota    [bit],
    
    @SobotaRanajky              [bit],
    @SobotaObed                 [bit],
    @SobotaVecera               [bit],
    @SobotaVecera2              [bit],
    @UbytovanieSobotaNedela     [bit],
    @TichaTriedaSobotaNedela    [bit],
    
    @NedelaRanajky              [bit],
    @NedelaObed                 [bit],
    
    @Sach                       [bit],
    @PingPong                   [bit],
    @IdTricko                   [int] = null,
    @Sluziaci                   [bit],
    @Poznamka                   [nvarchar](max),
    
    @Internat1                  [bit],
    @Internat2                  [bit],
    @Preplacame                 [decimal](18,4),
    @InternatZadarmo            [bit],
    @RegistraciaZadarmo         [bit],
    @JedloZadarmo               [bit],
    
    @Prisli                     [bit],
    @IstoPride                  [bit],
    @Donation                   [decimal](18,4),
    @NewId                      [int] output
AS
BEGIN
    declare @now datetime = getutcdate();
    declare @idVariabilny int;
    
    if @idUser is null
    begin
        -- insert
        insert into [dbo].[receivable]
            ([email], [amount], [donation], [idCurrency], [needHelp], [dtModified])
        values
            (@Email, 0, @Donation, 1, 0, @now);
        
        set @idVariabilny = scope_identity();

        insert into [dbo].[registracia]
           ([IdVariabilny],[Meno],[Priezvisko],[Email],[Telefon],[IdZbor],[InyZbor],
            [PiatokVecera],[PiatokVecera2],[UbytovaniePiatokSobota],[TichaTriedaPiatokSobota],
            [SobotaRanajky],[SobotaObed],[SobotaVecera],[SobotaVecera2],[UbytovanieSobotaNedela],[TichaTriedaSobotaNedela],
            [NedelaRanajky],[NedelaObed],[Sach],[PingPong],[IdTricko],
            [Sluziaci],
            [Poznamka],[DtModified],[Internat1],[Internat2],[Preplacame],[InternatZadarmo],[RegistraciaZadarmo],[JedloZadarmo],
            [DtPlatba],[Prisli],[IstoPride])
        values
           (@idVariabilny,@Meno,@Priezvisko,@Email,@Telefon,@IdZbor,@InyZbor,
            @PiatokVecera,@PiatokVecera2,@UbytovaniePiatokSobota,@TichaTriedaPiatokSobota,
            @SobotaRanajky,@SobotaObed,@SobotaVecera,@SobotaVecera2,@UbytovanieSobotaNedela,@TichaTriedaSobotaNedela,
            @NedelaRanajky,@NedelaObed,@Sach,@PingPong,@IdTricko,
            case when @Sluziaci = 1 then 'Dobrovolnik' else '' end,
            @Poznamka,@now,@Internat1,@Internat2,@Preplacame,@InternatZadarmo,@RegistraciaZadarmo,@JedloZadarmo,
            null,@Prisli,@IstoPride);
            
        set @NewId = scope_identity();
    end
    else
    begin
        -- update
        update [dbo].[registracia] set
            [Meno] = @Meno,
            [Priezvisko] = @Priezvisko,
            [Email] = @Email,
            [Telefon] = @Telefon,
            [IdZbor] = @IdZbor,
            [InyZbor] = @InyZbor,
            [PiatokVecera] = @PiatokVecera,
            [PiatokVecera2] = @PiatokVecera2,
            [UbytovaniePiatokSobota] = @UbytovaniePiatokSobota,
            [TichaTriedaPiatokSobota] = @TichaTriedaPiatokSobota,
            [SobotaRanajky] = @SobotaRanajky,
            [SobotaObed] = @SobotaObed,
            [SobotaVecera] = @SobotaVecera,
            [SobotaVecera2] = @SobotaVecera2,
            [UbytovanieSobotaNedela] = @UbytovanieSobotaNedela,
            [TichaTriedaSobotaNedela] = @TichaTriedaSobotaNedela,
            [NedelaRanajky] = @NedelaRanajky,
            [NedelaObed] = @NedelaObed,
            [Sach] = @Sach,
            [PingPong] = @PingPong,
            [IdTricko] = @IdTricko,
            [Sluziaci] = case when @Sluziaci = 1 then 'Dobrovolnik' else '' end,
            [Poznamka] = @Poznamka,
            [Internat1] = @Internat1,
            [Internat2] = @Internat2,
            [Preplacame] = @Preplacame,
            [InternatZadarmo] = @InternatZadarmo,
            [RegistraciaZadarmo] = @RegistraciaZadarmo,
            [JedloZadarmo] = @JedloZadarmo,
            [Prisli] = @Prisli,
            [IstoPride] = @IstoPride
        where [Id] = @idUser;

        select @idVariabilny = [IdVariabilny] 
        from [dbo].[registracia]
        where [Id] = @idUser;
    
        update [dbo].[receivable] set
            [donation] = @Donation
        where [Id] = @idVariabilny;
    end
END
GO
