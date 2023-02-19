import 'package:flutter/material.dart';
import 'dart:convert' show ascii, base64, json, jsonEncode, jsonDecode;
import 'package:deneme_app/utils.dart';
import 'package:http/http.dart' as http;

class GameDetail extends StatelessWidget {
  final assetPath, gamePrice, gameName, gameDescription, gameCategory, gameId;
  
  GameDetail({this.assetPath, this.gamePrice, this.gameName, this.gameDescription, this.gameCategory, this.gameId});

  void displayDialog(context, title, text) => showDialog(
        context: context,
        builder: (context) =>
            AlertDialog(title: Text(title), content: Text(text)),
  );


  Future<bool?> postCart(int gameId) async {
    String? jwt_token = await storage.read(key: 'token');
    //final uri = Uri.parse("http://10.0.2.2:5000/api/Games/GetAll");
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Cart/Post?gameId=$gameId");
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



  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.white,
        elevation: 0.0,
        centerTitle: true,
        leading: IconButton(
          icon: Icon(Icons.arrow_back, color: Color(0xFF545D68)),
          onPressed: () {
            Navigator.of(context).pop();
          },
        ),
        
        
      ),

      body: ListView(
        children: [
          SizedBox(height: 15.0),
          Padding(
            padding: EdgeInsets.only(left: 20.0),
            child: Text(
              'Game Details',
              style: TextStyle(
                      fontFamily: 'Varela',
                      fontSize: 42.0,
                      fontWeight: FontWeight.bold,
                      color: Color(0xFFF17532))
            ),
          ),
            SizedBox(height: 15.0),
            Hero(
              tag: assetPath,
              child: Container(
                  height: 75.0,
                  width: 75.0,
                  decoration: BoxDecoration(
                    image: DecorationImage(
                        image: NetworkImage(assetPath, scale: 0.6,), fit: BoxFit.contain),
                  ),
              ),
            ),
            SizedBox(height: 20.0),
            Center(
              child: Text(gamePrice.toString(),
                  style: TextStyle(
                      fontFamily: 'Varela',
                      fontSize: 22.0,
                      fontWeight: FontWeight.bold,
                      color: Color(0xFFF17532))),
            ),
            SizedBox(height: 10.0),
            Center(
              child: Text(gameName,
                  style: TextStyle(
                      color: Color(0xFF575E67),
                      fontFamily: 'Varela',
                      fontSize: 24.0)),
            ),
            SizedBox(height: 20.0),
            Center(
              child: Container(
                width: MediaQuery.of(context).size.width - 50.0,
                child: Text(gameDescription,
                textAlign: TextAlign.center,
                style: TextStyle(
                      fontFamily: 'Varela',
                      fontSize: 16.0,
                      color: Color(0xFFB4B8B9))
                ),
              ),
            ),
            SizedBox(height: 20.0),
            Center(
              child: Container(
                width: MediaQuery.of(context).size.width - 50.0,
                height: 50.0,
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(25.0),
                  color: Color(0xFFF17532)
                ),
                child: InkWell(
                  onTap: () async {
                    bool? success = await postCart(gameId);
                    if (success != null && success == true) {
                      displayDialog(context, "Sepete eklendi", "Başarılı");
                    } else {
                      displayDialog(context, "Sepete eklenemedi", "başarısız");
                    }
                  },
                  child: Center(
                    child: Text('Add to cart',
                      style: TextStyle(
                        fontFamily: 'Varela',
                        fontSize: 14.0,
                        fontWeight: FontWeight.bold,
                        color: Colors.white
                  ),
                    )
                  ),
                )
              )
            )
        ]
      ),
    );
  }
}
