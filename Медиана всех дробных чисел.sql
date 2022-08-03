USE b1testtask1;

SELECT AVG(dd.RandomPositiveDouble) as median_val
FROM (
SELECT CustomRecords.RandomPositiveDouble, @rownum:=@rownum+1 as `row_number`, @total_rows:=@rownum
  FROM CustomRecords, (SELECT @rownum:=0) r
  WHERE CustomRecords.RandomPositiveDouble is NOT NULL
  ORDER BY CustomRecords.RandomPositiveDouble
) as dd
WHERE dd.row_number IN ( FLOOR((@total_rows+1)/2), FLOOR((@total_rows+2)/2) );
