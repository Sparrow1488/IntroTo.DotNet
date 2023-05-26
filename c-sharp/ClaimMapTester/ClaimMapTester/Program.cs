using System.Collections.Specialized;

var valueFromPlc = 255;
Console.WriteLine(valueFromPlc);

var claims = new BitVector32(valueFromPlc);
Console.WriteLine(claims.ToString());

int manual = BitVector32.CreateMask();
int auto = BitVector32.CreateMask(manual);
int config = BitVector32.CreateMask(auto);
int movedrive = BitVector32.CreateMask(config);
int edituser = BitVector32.CreateMask(movedrive);

Console.WriteLine("\tClaims[auto]  до: {0}", claims[auto] );

claims[manual] = false;
claims[auto] = false;
claims[config] = false;
claims[movedrive] = false;
claims[edituser] = false;

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