CREATE Function dbo.udf_StripHTML
(@HTMLText varchar(Max)) 
Returns VarChar(Max) 
As
Begin
	/*
    declare @textXML xml
    declare @result varchar(max)
    set @textXML = @text;
    with doc(contents) as
    (
        select chunks.chunk.query('.') from @textXML.nodes('/') as chunks(chunk)
    )
    select @result = contents.value('.', 'varchar(max)') from doc
    return @result
	*/	
	
    DECLARE @Start INT
    DECLARE @End INT
    DECLARE @Length INT
    SET @Start = CHARINDEX('<',@HTMLText)
    SET @End = CHARINDEX('>',@HTMLText,CHARINDEX('<',@HTMLText))
    SET @Length = (@End - @Start) + 1
    WHILE @Start > 0 AND @End > 0 AND @Length > 0
    BEGIN
        SET @HTMLText = STUFF(@HTMLText,@Start,@Length,'')
        SET @Start = CHARINDEX('<',@HTMLText)
        SET @End = CHARINDEX('>',@HTMLText,CHARINDEX('<',@HTMLText))
        SET @Length = (@End - @Start) + 1
    END
    RETURN LTRIM(RTRIM(@HTMLText))
End
