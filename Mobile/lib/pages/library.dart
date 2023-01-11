//import 'dart:html';
import 'dart:async';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:deneme_app/pages/game_details.dart';
import 'dart:convert' show ascii, base64, json, jsonEncode, jsonDecode;
import 'package:deneme_app/utils.dart';
import 'package:http/http.dart' as http;
import 'package:dio/dio.dart';
import 'package:url_launcher/url_launcher.dart';
//import 'package:downloads_path_provider_28/downloads_path_provider_28.dart';
import 'package:path_provider/path_provider.dart';
import 'package:permission_handler/permission_handler.dart';

class GenresClass {
  final int genreID;
  final String categoryName;
  GenresClass({
    required this.genreID,
    required this.categoryName,
  });

  factory GenresClass.fromJson(Map<String, dynamic> json) {
    return GenresClass(
      genreID: json["genreID"], 
      categoryName: json["categoryName"]
    );
  }
}

class GameClass {
  final int id;
  final String imageUrl;
  final String gameName;
  final double gamePrice;
  final String description;
  final String publisher;
  final bool childrenSuitable;
  final String availableAgeScala;
  final String releaseDate;
  final double rating;
  final String languageOption;
  final List<GenresClass> genres;
  GameClass({
    required this.id,
    required this.imageUrl,
    required this.gameName,
    required this.gamePrice,
    required this.description,
    required this.publisher,
    required this.childrenSuitable,
    required this.availableAgeScala,
    required this.releaseDate,
    required this.rating,
    required this.languageOption,
    required this.genres,
  });

  factory GameClass.fromJson(Map<String, dynamic> json) {
    var list = json["genres"] as List;
    return GameClass(
      id: json["id"], 
      imageUrl: json["imageUrl"],
      gameName: json["gameName"], 
      gamePrice: json["gamePrice"], 
      description: json["description"], 
      publisher: json["publisher"], 
      childrenSuitable: json["childrenSuitable"], 
      availableAgeScala: json["availableAgeScala"], 
      releaseDate: json["releaseDate"], 
      rating: json["rating"], 
      languageOption: json["languageOption"],
      genres: list.map((i) => GenresClass.fromJson(i)).toList(),
      );
  }
}


class GameResponse {
  late final List<GameClass> response;
  //final int code;
  GameResponse({
    required this.response,
    //required this.code,
  });

  factory GameResponse.fromJson(Map<String, dynamic> json) {
    var list = json["games"] as List;
    return GameResponse(
      response: list.map((i) => GameClass.fromJson(i)).toList(),
      //code: json["code"],
    );
  }
}

class CardData{
  String name = "";
  double price = 0.0; 
  //String rating = ""; 
  String description = "";
  String category = "";
  String imgPath = "";
  int id = -1;
  CardData(String name,double price,String description,String category,String imgPath, int id){
    this.name = name;
    this.price = price;
    this.description = description;
    this.category = category;
    this.imgPath = imgPath;
    this.id = id;
  }
}


String downloadPath = "images/download.png";

class Library extends StatelessWidget {
  Library({super.key});
  
  Future<List<CardData?>?> getLibrary() async {
    String? jwt_token = await storage.read(key: 'token');
    //final uri = Uri.parse("http://10.0.2.2:5000/api/Games/GetAll");
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Library/GetAll");
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
      List<CardData?> response = [];
      for(int i = 0; i < result.response.length; i++){
        CardData temp = CardData(result.response[i].gameName, result.response[i].gamePrice, result.response[i].description, result.response[i].genres[0].categoryName, result.response[i].imageUrl, result.response[i].id);
        response.add(temp);
      }
      return response;
      } 
    return null; //string must be returned so i changed it into string
  }

  Future<String?> downloadFile(int id) async {
    String? jwt_token = await storage.read(key: 'token');
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Library/Download?id=$id");
    var res = await http.get(
      uri,
      headers: {
        "Authorization": "bearer $jwt_token",
        "Accept": "application/json",
        "content-type": "application/json"
      },
    );
    if (res.statusCode == 200){
      //Directory appDocdir = await getApplicationDocumentsDirectory();
      
      String filePath = '/storage/emulated/0/Download';
      var file = File(filePath+"/game.apk");
      return res.body;
      } 
    return null;
  }


  Future<bool?> downloadGame(int id) async {
    String? jwt_token = await storage.read(key: 'token');
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Library/Download?id=$id");
    var res = await http.get(
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
      backgroundColor: Color(0xFFFCFAF8),
      body: Center(
        child: FutureBuilder(
          future: getLibrary(),
          builder:(context, snapshot) {
            if (snapshot.connectionState == ConnectionState.done){
              if(snapshot.hasError){
                Text("there is error");
              }
              else if (snapshot.hasData){
                var data = snapshot.data as List<CardData?>;
                return ListView.builder(
                    padding: EdgeInsets.only(right: 15.0,left: 15.0),
                    itemCount: data.length,
                    itemBuilder: (BuildContext context, int index) {
                      return Container(
                        height: 225,
                        margin: EdgeInsets.all(2),
                        child: Center(
                            child: _buildCard(data[index]!.name, data[index]!.price, data[index]!.description, data[index]!.category, data[index]!.imgPath,context, data[index]!.id),
                          )
                        );
                    }
                  );
              }
            }
            return const CircularProgressIndicator();
              
          },
        )
      )
    );
  }

  Widget _buildCard(String name, double price, String description,
      String category, String imgPath, context, int gameId) {
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
                        image: NetworkImage(imgPath, scale: 0.6,), fit: BoxFit.contain),
                  ),
                ),
              ),
              SizedBox(height: 7.0),
              Text(category,
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
                  child: Container(color: Color(0xFFEBEBEB), height: 1.0)),
              Hero(
                tag: imgPath,
                child: InkWell(
                  onTap: () async {
                    final link;
                    link = await downloadFile(gameId);
                    final uri = Uri.parse(link);
                    await launchUrl(uri);
                  },
                  child: Container(
                    height: 40.0,
                    width: 40.0,
                    decoration: BoxDecoration(
                      image: DecorationImage(
                          image: AssetImage(downloadPath), fit: BoxFit.contain),
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
