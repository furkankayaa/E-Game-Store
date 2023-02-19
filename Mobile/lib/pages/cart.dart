import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'dart:convert' show ascii, base64, json, jsonEncode, jsonDecode;
import 'package:deneme_app/utils.dart';
import 'package:http/http.dart' as http;
import 'package:file_picker/file_picker.dart';

class GameClass {
  final int cartId;
  final int gameId;
  final String gameName;
  final double gamePrice;
  final String imageUrl;
  final String publisher;
  GameClass({
    required this.cartId,
    required this.gameId,
    required this.gameName,
    required this.gamePrice,
    required this.imageUrl,
    required this.publisher
  });

  factory GameClass.fromJson(Map<String, dynamic> json) {
    return GameClass(
      cartId: json["id"],
      gameId: json["gameId"], 
      gameName: json["gameName"], 
      gamePrice: json["gamePrice"],  
      imageUrl: json["imageUrl"],
      publisher: json["publisher"]
      );
  }
}


class GameResponse {
  late final List<GameClass> response;
  GameResponse({
    required this.response,
  });

  factory GameResponse.fromJson(List<dynamic> json) {
    return GameResponse(
      response: json.map((i) => GameClass.fromJson(i)).toList(),
    );
  }
}


class CardCartData {
  int cartId = -1;
  int gameId = -1;
  String gameName = "";
  double gamePrice = 0.0;
  String imageUrl = "";
  String publisher = "";
  CardCartData(int cartId, int gameId, String gameName, double gamePrice, String imgUrl, String publisher) {
    this.cartId = cartId;
    this.gameId = gameId;
    this.gameName = gameName;
    this.gamePrice = gamePrice;
    this.imageUrl = imgUrl;
    this.publisher = publisher;
  }
}

String trashPath = "images/trash.png";

class UserCart extends StatefulWidget {
  UserCart({super.key});
  @override
  State<UserCart> createState() => _UserCartState();
}

class _UserCartState extends State<UserCart>{


  Future<List<CardCartData?>?> getCartAll() async {
    String? jwt_token = await storage.read(key: 'token');
    //final uri = Uri.parse("http://10.0.2.2:5000/api/Games/GetAll");
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Cart/GetAll");
    var res = await http.get(
      uri,
      headers: {
        "Authorization": "bearer $jwt_token",
        "Accept": "application/json",
        "content-type": "application/json"
      },
    );
    if (res.statusCode == 200){
      var result = GameResponse.fromJson(jsonDecode(res.body));
      List<CardCartData?> response = [];
      for(int i = 0; i < result.response.length; i++){
        CardCartData temp = CardCartData(result.response[i].cartId, result.response[i].gameId, result.response[i].gameName, result.response[i].gamePrice, result.response[i].imageUrl, result.response[i].publisher);
        response.add(temp);
      }
      return response;
      } 
    return null;
  }

  Future<bool?> deleteGame(int id) async {
    String? jwt_token = await storage.read(key: 'token');
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Cart/Delete?id=$id");
    var res = await http.delete(
      uri,
      headers: {
        "Authorization": "bearer $jwt_token",
        "Accept": "application/json",
        "content-type": "application/json"
      },
    );
    if (res.statusCode == 200){
      return true;
    } 
    return null;
  }
  Future<bool?> postOrder() async {
    String? jwt_token = await storage.read(key: 'token');
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Order/Post");
    var res = await http.post(
      uri,
      headers: {
        "Authorization": "bearer $jwt_token",
        "Accept": "application/json",
        "content-type": "application/json"
      },
    );
    if (res.statusCode == 200){
      return true;
    } 
    return null;
  }

  var data;

  @override
  Widget build(BuildContext context) {
    double baseWidth = 393;
    double fem = MediaQuery.of(context).size.width / baseWidth;
    double ffem = fem * 0.97;
    
    return Scaffold(
        backgroundColor: Color(0xFFFCFAF8),
        body: Center(
          child: FutureBuilder(
            future: getCartAll(),
            builder: (context, snapshot) {
              if (snapshot.connectionState == ConnectionState.done){
                if(snapshot.hasError){
                  Text("there is error");
                }
                else if (snapshot.hasData){
                  data = snapshot.data as List<CardCartData?>;
                  return Container(
              width: double.infinity,
              child: SingleChildScrollView(
                child: Container(
                  width: double.infinity,
                  decoration: BoxDecoration(
                    color: Color(0xffc7e3d1),
                  ),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    mainAxisSize: MainAxisSize.max,
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Container(
                          padding: EdgeInsets.only(
                              right: 30.0, left: 20.0, top: 50.0, bottom: 20.0),
                          width: MediaQuery.of(context).size.width - 30.0,
                          height: MediaQuery.of(context).size.height - 20.0,
                          
                          child: ListView.builder(
                              padding: EdgeInsets.only(right: 15.0, left: 15.0),
                              itemCount: data.length,
                              itemBuilder: (BuildContext context, int index) {
                                return Container(
                                    height: 225,
                                    margin: EdgeInsets.all(2),
                                    child: Center(
                                      child: _buildCard(data[index]!.gameName, data[index]!.gamePrice, data[index]!.imageUrl, data[index]!.cartId, context),
                                    ));
                              })),
                      Container(
                        // autogroupuzkwRES (VjrYn9XJgmzGLGosx9UZKw)
                        margin: EdgeInsets.fromLTRB(
                            0 * fem, 0 * fem, 0 * fem, 15.74 * fem),
                        width: double.infinity,
                        height: 224.26 * fem,
                        padding: EdgeInsets.only(
                              right: 10.0, left: 20.0, top: 0.0, bottom: 0.0),
                        child: Stack(
                          children: [
                            Positioned(
                              // rectangle14sML (23:11)
                              left: 0 * fem,
                              top: 0 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 358 * fem,
                                  height: 205 * fem,
                                  child: Container(
                                    decoration: BoxDecoration(
                                      borderRadius: BorderRadius.circular(20 * fem),
                                      color: Color(0xff88c3a7),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // rectangle16fnz (23:22)
                              left: 31 * fem,
                              top: 32 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 207 * fem,
                                  height: 36 * fem,
                                  child: Container(
                                    decoration: BoxDecoration(
                                      borderRadius: BorderRadius.circular(10 * fem),
                                      color: Color(0xffffffff),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // rectangle18XaJ (23:24)
                              left: 31 * fem,
                              top: 155 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 96 * fem,
                                  height: 36 * fem,
                                  child: Container(
                                    decoration: BoxDecoration(
                                      borderRadius: BorderRadius.circular(10 * fem),
                                      color: Color(0xff000000),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // rectangle19jwG (23:25)
                              left: 142 * fem,
                              top: 155 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 96 * fem,
                                  height: 36 * fem,
                                  child: Container(
                                    decoration: BoxDecoration(
                                      borderRadius: BorderRadius.circular(10 * fem),
                                      color: Color(0xff000000),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // nameoncardm7G (23:27)
                              left: 31 * fem,
                              top: 75 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 104 * fem,
                                  height: 19 * fem,
                                  child: Text(
                                    'Name on Card\n',
                                    style: SafeGoogleFont(
                                      'Inter',
                                      fontSize: 15 * ffem,
                                      fontWeight: FontWeight.w700,
                                      height: 1.2125 * ffem / fem,
                                      color: Color(0xff4f723e),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // rectangle17vPU (23:23)
                              left: 31 * fem,
                              top: 96 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 207 * fem,
                                  height: 36 * fem,
                                  child: Container(
                                    decoration: BoxDecoration(
                                      borderRadius: BorderRadius.circular(10 * fem),
                                      color: Color(0xff000000),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // cardnumberyMk (23:26)
                              left: 31 * fem,
                              top: 10 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 98 * fem,
                                  height: 19 * fem,
                                  child: Text(
                                    'Card Number',
                                    style: SafeGoogleFont(
                                      'Inter',
                                      fontSize: 15 * ffem,
                                      fontWeight: FontWeight.w700,
                                      height: 1.2125 * ffem / fem,
                                      color: Color(0xff4f723e),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // expdateNPt (23:28)
                              left: 31 * fem,
                              top: 136 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 60 * fem,
                                  height: 17 * fem,
                                  child: Text(
                                    'Exp date',
                                    style: SafeGoogleFont(
                                      'Inter',
                                      fontSize: 14 * ffem,
                                      fontWeight: FontWeight.w700,
                                      height: 1.2125 * ffem / fem,
                                      color: Color(0xff4f723e),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // codeZUN (23:30)
                              left: 142 * fem,
                              top: 136 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 37 * fem,
                                  height: 17 * fem,
                                  child: Text(
                                    'Code',
                                    style: SafeGoogleFont(
                                      'Inter',
                                      fontSize: 14 * ffem,
                                      fontWeight: FontWeight.w700,
                                      height: 1.2125 * ffem / fem,
                                      color: Color(0xff4f723e),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // background2xWW (23:32)
                              left: 264 * fem,
                              top: 13 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 80 * fem,
                                  height: 65 * fem,
                                  child: Image.asset(
                                    'images/chip.png',
                                    fit: BoxFit.cover,
                                  ),
                                ),
                              ),
                            ),
                            Positioned(
                              // creditcardaggregatorvisa115639 (23:34)
                              left: 253 * fem,
                              top: 122 * fem,
                              child: Align(
                                child: SizedBox(
                                  width: 100 * fem,
                                  height: 102.26 * fem,
                                  child: Image.asset(
                                    'images/visa.png',
                                    fit: BoxFit.cover,
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Container(
                        // autogroup1b9bSS6 (E9qcYfMwEincs5WPUF1b9B)
                        margin: EdgeInsets.fromLTRB(
                            61 * fem, 0 * fem, 67 * fem, 0 * fem),
                        width: double.infinity,
                        height: 63 * fem,
                        decoration: BoxDecoration(
                          color: Color(0xff4f723e),
                          borderRadius: BorderRadius.circular(28 * fem),
                        ),
                        child: InkWell(
                          onTap: () async {
                            await postOrder();
                            setState(() {
                              
                            });
                          },
                          child: Center(
                            child: Text(
                              'Checkout',
                              style: SafeGoogleFont(
                                'Inter',
                                fontSize: 36 * ffem,
                                fontWeight: FontWeight.w700,
                                height: 1.2125 * ffem / fem,
                                color: Color(0xffffffff),
                              ),
                            ),
                          ),
                        ),
                      ),
                      SizedBox(height: 20.0,)
                    ],
                  ),
                ),
              ),
              );
                }
              }
            return const CircularProgressIndicator();
            },
          ),
        ));
  }


  Widget _buildCard(String name, double price, String imgPath, int cartId, context) {
    return Padding(
      padding: EdgeInsets.only(top: 5.0, bottom: 5.0, left: 5.0, right: 5.0),
      child: InkWell(
        onTap: () {},
        child: Container(
          decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(15.0),
              boxShadow: [
                BoxShadow(
                    color: Colors.grey.withOpacity(0.2),
                    spreadRadius: 3.0,
                    blurRadius: 5.0)
              ],
              color: Colors.white),
          child: Column(
            children: [
              SizedBox(height: 15.0),
              Padding(
                padding: EdgeInsets.all(5.0),
              ),
              Hero(
                tag: imgPath,
                child: Container(
                  height: 75.0,
                  width: 75.0,
                  decoration: BoxDecoration(
                    image: DecorationImage(
                        image: NetworkImage(imgPath, scale: 0.6), fit: BoxFit.contain),
                  ),
                ),
              ),
              SizedBox(height: 7.0),
              Text(price.toString(),
                  style: TextStyle(
                      color: Color(0xFFCC8053),
                      fontFamily: 'Varela',
                      fontSize: 14.0)),
              Text(name,
                  style: TextStyle(
                      color: Color(0xFF575E67),
                      fontFamily: 'Varela',
                      fontSize: 14.0)),
              Padding(
                padding: EdgeInsets.all(8.0),
                child: Container(color: Color(0xFFEBEBEB), height: 1.0),
              ),
              Hero(
                tag: imgPath,
                child: InkWell(
                  onTap: () async {
                      var temp = CardCartData(-1, -1, name, price, imgPath, "str");
                      await deleteGame(cartId);
                    setState(() {                      
                    });
                  },
                  child: Container(
                    height: 40.0,
                    width: 40.0,
                    decoration: BoxDecoration(
                      image: DecorationImage(
                          image: AssetImage(trashPath), fit: BoxFit.contain),
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
