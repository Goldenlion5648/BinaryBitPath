using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{


    public static int playerCoordsX = 0;
    public static int playerCoordsY = 0;

    public static AudioManager audioManager;

    public static string levelContentHack = "";


    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioController").GetComponent<AudioManager>();
        print("found this audio manager" + audioManager.gameObject.name);

        levelContentHack = @">
0010#basic right shift0
0000
1000

<
111010000
000000000#basic left shift1
000111010

>
0000011
0000000#bits can be chopped2
1111100

<>
000001100
000000000#bits can be chopped3
111110000

<a
1010000
0000101#teach and4
0001111

<a
0110000
0111110#teach and5
1111111

<>a
00110010
11110111#more and6
11111111

<>o
1111
0011#teach or7
0000

<>o
1111
1001#more or8
0000

<>oa
00010010
00001111#more or9
01100000

x<
1110000
1011100#teach xor10
1000000

x<
10001000
11110000#teach xor11
11001100

x<>
0010101
1011100#teach xor12
0000001

x>
0010001
1100011#teach xor13
1111100

<>x
11011000
00101110#>>>><<<x<<14
00111000

<>o
0001011111
0001011101#o>>o15
0000000101

>o
0000011111
0001010011#o>o>o>>16
0000000001

<ox
01100100001
00011110101#<oxo<<x17
00000001000

<>ao
0000111100
0000111101#oa><a>>>xa18
0000000011

>aox
0000111100
0000111101#oa>a>>>>x19
0000000011

<>n
01100
00000#oa>a>>>>x20
00001

<>no
11111110010
00001010000#>>><<<<<<n>>>21
00000001011

<>nx
11111110010
00001010000#22
00000001011

>anox
10111111111
01011100101#no>>xan23
00000001111

4<>o
1111
0011#o<<o24
0000

3>n
01111111101
10111111001#>n>25
00000001010

5>x
01111000101
01110110010#x>>>x26
00000001101

2<>on
011110
000011#n>27
000011

5>ox
00111001111
00100001111#x>x>o28
00000000000

6>x
00111011110
00111000010#x>>>>x29
00000001100

6>ox
11111111011
11010111011#x>x>>o30
00000000000

5>nox
11010100101
00101011010#ox>on31
00000001110

6<anx
10000100101
10001110101#a<<<<x32
00000000111

7>nx
00110011010
00111111010#xn>>>>x33
00000001101

7<ano
00111101100
11110000100#onoan<<34
00000000111

6<>ox
00101110010
00101100000#<<x>>o35
00000001010

7>ox
00101010101
00101110111#x>ox>>x36
00000000110

8>nox
11100100100
11001000101#xn>o>>>n37
00000001010

6<>an
11001001111
01101101110#na>><n38
00000001101

7<>ano
11111110101
00100011011#>o>a><n39
00000001000

6<>ax
11100011010
01101011011#xa>>x<40
00000000100

9>x
00110110010
00100010000#x>>x>>x>x41
00000000111

7<>ano
11100010111
11000101101#oan>><n42
00000010000

7<>nox
11110111111
00000101101#x>n>x<o43
00000000011

8<anox
11010111010
11111101010#oan<<<<x44
00000000011

9<>aox
00100010110
00000100100#oa<<<x<x>45
00000001110

10<>nox
11001000111
00101000001#x>x>o>x><n46
00000000001

10<>an
01001001111
01001111110#nan>>><<<n47
00000110100

11>anx
10011010100
01111110100#xn>>an>>>xn48
00000110011

12<>nox
00011011011
11111101000#x>>ox<xn<<xn49
00000000000
";
        // makeInitialMap();
    }

    

    // public static void changeMap(int fromY, int fromX, int toY, int toX)
    // {
    //     roomMap[(fromY, fromX)] = (toY, toX);
    // }

    public static int doubleDimToSingle(int fromY, int fromX, int dim)
    {
        return fromY * dim + fromX;
    }

    public static (int, int) singleDimToDouble(int oneDim, int dim)
    {
        return (oneDim / dim, oneDim % dim);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
