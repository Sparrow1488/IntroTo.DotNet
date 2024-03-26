using System.Collections.Specialized;

var permissions = new List<string>()
{
    "Manual",
    "Auto",
    "Config",
    "EditUsers"
};

var mask = new List<int>();

var previous = 0;
permissions.ForEach(_ =>
{
    previous = BitVector32.CreateMask(previous);
    mask.Add(previous);
});

Console.WriteLine(mask);






var valueFromPlc = 255;
Console.WriteLine(valueFromPlc);
    
var claims = new BitVector32(valueFromPlc);
Console.WriteLine(claims.ToString());

var manual = BitVector32.CreateMask();
var auto = BitVector32.CreateMask(manual);
var config = BitVector32.CreateMask(auto);
var moveDrive = BitVector32.CreateMask(config);
var editUser = BitVector32.CreateMask(moveDrive);
var editPerformance = BitVector32.CreateMask(editUser);
var editScene = BitVector32.CreateMask(editPerformance);

Console.WriteLine("\tClaims[auto]  до: {0}", claims[auto] );

claims[manual] = false;
claims[auto] = false;
claims[config] = false;
claims[moveDrive] = true;
claims[editUser] = false;
claims[editPerformance] = false;
claims[editScene] = true;

Console.WriteLine("\tClaims[auto]  после: {0}", claims[auto]);

Console.WriteLine(claims.ToString());

var valueToPlc = claims.Data;
Console.WriteLine(valueToPlc);




























// Console.WriteLine("Hello, World!");
//
// /*
//  * bin[0] - ручной режим
//  * bin[1] - автоматический режим
//  * bin[2] - конфигурационный режим
//  * bin[3] - езда приводами
//  * bin[4] - редактирование пользователей
//  */
// var baseBinClaims = ToFixedSize("000000100101110");
// var userClaimsValue = Convert.ToUInt32(baseBinClaims, 2);
//
//
// var binClaims = Convert.ToString(userClaimsValue, 2);
// var convertedBindClaims = ToFixedSize(binClaims);
//
// Console.WriteLine("Base: \t\t" + baseBinClaims);
// Console.WriteLine("Now: \t\t" + convertedBindClaims);
// Console.WriteLine("Word value: \t" + userClaimsValue);
//
// string ToFixedSize(string binValue)
// {
//     return binValue.PadLeft(32, '0');
// }