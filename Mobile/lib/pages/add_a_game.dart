import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:deneme_app/utils.dart';
import 'dart:convert' show ascii, base64, json, jsonEncode, jsonDecode;
import 'package:deneme_app/utils.dart';
import 'package:http/http.dart' as http;
import 'package:file_picker/file_picker.dart';
import 'dart:io';
import 'package:http_parser/http_parser.dart';

class UserAddAGame extends StatefulWidget {
  UserAddAGame({super.key});
  @override
  State<UserAddAGame> createState() => _UserAddGameState();
}

class _UserAddGameState extends State<UserAddAGame>{
  // Initial Selected Value
  String dropdownvalue = 'Sports';  

  // List of items in our dropdown menu
  var items = [   
    'Sports',
    'Horror',
    'FPS',
    'MOBA',
    'Strategy',
    'Action',
    'Simulation',
    'TPS'
  ];
  void displayDialog(context, title, text) => showDialog(
        context: context,
        builder: (context) =>
            AlertDialog(title: Text(title), content: Text(text)),
  );
  
  final TextEditingController _gameNameController = TextEditingController();
  final TextEditingController _descriptionController = TextEditingController();
  final TextEditingController _priceController = TextEditingController();
  File? photo;
  File? apk;
  Future<bool?> postGame(String gameName, String description, int category, File photo, File apk, double price) async {
    //final uri = Uri.parse("http://10.0.2.2:5000/api/Auth/UserLogin");
    String? jwt_token = await storage.read(key: 'token');
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/PublishRequest/Post");
    
    var res = await http.MultipartRequest("POST", uri);
    res.headers.addAll({"Authorization": "Bearer $jwt_token"});

     
    res.files.add(await http.MultipartFile.fromPath("ApkFile", apk.path));
    res.files.add(await http.MultipartFile.fromPath("ImageFile", photo.path));
    
    res.fields['GameName'] = gameName;
    res.fields["GamePrice"] = price.toString();
    res.fields["Description"] = description;
    res.fields["ChildrenSuitable"] = "true";
    res.fields["AvailableAgeScala"] = "string";
    res.fields["LanguageOption"] = "string";
    res.fields["Genres"] = category.toString(); 

    /*
    res.fields.addAll({'game': jsonEncode({
                          "gameName": gameName,
                          "gamePrice": price,
                          "description": description,
                          "publisher": "string",
                          "childrenSuitable": true,
                          "availableAgeScala": "string",
                          "releaseDate": "2023-01-01T09:14:56.915Z",
                          "rating": 0,
                          "languageOption": "string",
                          "isApproved": true
                          },
                      )
                    });
      res.fields.addAll({"genreIds": jsonEncode([category])});
    */
    var response = await res.send();
    /*
    var res = await http.post(
      uri,
      headers: {
        "Authorization": "bearer $jwt_token",
        "Accept": "application/json",
        "content-type": "application/json"
      },
      body: jsonEncode({'game': {
                          "imageUrl": photo,
                          "gameName": gameName,
                          "gamePrice": price,
                          "description": description,
                          "publisher": "string",
                          "childrenSuitable": true,
                          "availableAgeScala": "string",
                          "releaseDate": "2023-01-01T09:14:56.915Z",
                          "rating": 0,
                          "languageOption": "string",
                          "ApkFile": apk,
                          "isApproved": true
                          },
                          "genreIds": [category]}),
    );
    */

    if (response.statusCode == 200){
      //var result = AccessToken.fromJson(jsonDecode(res.body));
      return true;
  
      } 
    return null; 
  }

  @override
  Widget build(BuildContext context) {
    double baseWidth = 393;
    double fem = MediaQuery.of(context).size.width / baseWidth;
    double ffem = fem * 0.97;
    return Scaffold(
      body: Container(
        width: double.infinity,
        child: SingleChildScrollView(
          child: Container(
            // addagamepagenKU (8:7)
            width: double.infinity,
            decoration: BoxDecoration (
              color: Color(0xffc7e3d1),
            ),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              mainAxisSize: MainAxisSize.max,
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Container(
                  // autogroup5cem3mC (VjrVmV3LcVsm8XZD495ceM)
                  padding: EdgeInsets.fromLTRB(25*fem, 12*fem, 25*fem, 54*fem),
                  width: double.infinity,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      SizedBox(height: 25.0,),
                      Container(
                        // nameRWr (14:17)
                        margin: EdgeInsets.fromLTRB(0*fem, 0*fem, 210*fem, 8*fem),
                        child: Text(
                          'Game Name',
                          style: SafeGoogleFont (
                            'Inter',
                            fontSize: 22*ffem,
                            fontWeight: FontWeight.w700,
                            height: 0.2125*ffem/fem,
                            letterSpacing: -0.44*fem,
                            color: Color(0xff4f723e),
                          ),
                        ),
                      ),
                      TextField(
                        textAlign: TextAlign.center,
                        controller: _gameNameController,
                        decoration: InputDecoration(
                          fillColor: Colors.grey.shade100,
                          filled: true,
                          //border: OutlineInputBorder(),
                          hintText: 'Game Name',
                          border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(10),),
                        ),
                      ),
                      Container(
                        // descriptioni8a (14:22)
                        margin: EdgeInsets.fromLTRB(0*fem, 0*fem, 207*fem, 8*fem),
                        child: Text(
                          'Description',
                          style: SafeGoogleFont (
                            'Inter',
                            fontSize: 22*ffem,
                            fontWeight: FontWeight.w700,
                            height: 1.2125*ffem/fem,
                            letterSpacing: -0.44*fem,
                            color: Color(0xff4f723e),
                          ),
                        ),
                      ),
                      TextField(
                        textAlign: TextAlign.center,
                        maxLines: 5,
                        minLines: 5,
                        controller: _descriptionController,
                        decoration: InputDecoration(
                          fillColor: Colors.grey.shade100,
                          filled: true,
                          //border: OutlineInputBorder(),
                          hintText: 'Game Description',
                          border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(10),),
                        ),
                      ),
                      Container(
                        // descriptioni8a (14:22)
                        margin: EdgeInsets.fromLTRB(0*fem, 0*fem, 207*fem, 8*fem),
                        child: Text(
                          'Category',
                          style: SafeGoogleFont (
                            'Inter',
                            fontSize: 22*ffem,
                            fontWeight: FontWeight.w700,
                            height: 1.2125*ffem/fem,
                            letterSpacing: -0.44*fem,
                            color: Color(0xff4f723e),
                          ),
                        ),
                      ),
                      DropdownButton(
                        
                        // Initial Value
                        value: dropdownvalue,
                        
                        // Down Arrow Icon
                        icon: const Icon(Icons.keyboard_arrow_down),   
                        
                        // Array list of items
                        items: items.map((String items) {
                          return DropdownMenuItem(
                            value: items,
                            child: Text(items),
                          );
                        }).toList(),
                        // After selecting the desired option,it will
                        // change button value to selected value
                        onChanged: (String? newValue) {
                          setState(() {
                            dropdownvalue = newValue!;
                          });
                        },
                      ),



                      /*
                      Container(
                        // categoryyii (14:27)
                        margin: EdgeInsets.fromLTRB(0*fem, 0*fem, 230*fem, 8*fem),
                        child: Text(
                          'Category',
                          style: SafeGoogleFont (
                            'Inter',
                            fontSize: 22*ffem,
                            fontWeight: FontWeight.w700,
                            height: 1.2125*ffem/fem,
                            letterSpacing: -0.44*fem,
                            color: Color(0xff4f723e),
                          ),
                        ),
                      ),
                      TextField(
                        textAlign: TextAlign.center,
                        //controller: ...,
                        decoration: InputDecoration(
                          fillColor: Colors.grey.shade100,
                          filled: true,
                          //border: OutlineInputBorder(),
                          hintText: 'Category',
                          border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(10),),
                        ),
                      ),
                      */
                      
                      Container(
                        // autogrouppwehTXQ (VjrVEfbMciFBbmoVUpPweh)
                        margin: EdgeInsets.fromLTRB(7*fem, 0*fem, 14*fem, 9*fem),
                        width: double.infinity,
                        child: Row(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                              // uploadphotowSa (14:32)
                              margin: EdgeInsets.fromLTRB(0*fem, 0*fem, 58*fem, 0*fem),
                              child: Text(
                                'Upload Photo',
                                style: SafeGoogleFont (
                                  'Inter',
                                  fontSize: 22*ffem,
                                  fontWeight: FontWeight.w700,
                                  height: 1.2125*ffem/fem,
                                  letterSpacing: -0.44*fem,
                                  color: Color(0xff4f723e),
                                ),
                              ),
                            ),
                            Text(
                              // uploadapkk98 (14:37)
                              'Upload APK',
                              style: SafeGoogleFont (
                                'Inter',
                                fontSize: 22*ffem,
                                fontWeight: FontWeight.w700,
                                height: 1.2125*ffem/fem,
                                letterSpacing: -0.44*fem,
                                color: Color(0xff4f723e),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Container(
                        // autogroupaez7f1C (VjrVPaWAyp5E5wwuG3AeZ7)
                        margin: EdgeInsets.fromLTRB(40*fem, 0*fem, 23*fem, 30*fem),
                        width: double.infinity,
                        child: Row(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            InkWell(
                              onTap: () async {
                                final result = await FilePicker.platform.pickFiles();
                                if(result == null) return;
                                PlatformFile platformfile = result.files.first;
                                photo = File(platformfile.path!);
                              },  
                              child: Container(
                                // fileuploadimageicon11563229050 (14:34)
                                margin: EdgeInsets.fromLTRB(0*fem, 1*fem, 100*fem, 0*fem),
                                width: 73*fem,
                                height: 72*fem,
                                child: Image.asset(
                                  'images/addimg.png',
                                  fit: BoxFit.cover,
                                ),
                              ),
                            ),
                            InkWell(
                              onTap: () async {
                                final result = await FilePicker.platform.pickFiles();
                                if(result == null) return;
                                PlatformFile platformfile = result.files.first;
                                apk = File(platformfile.path!);
                              },  
                              child: Container(
                                // upload1A6N (14:38)
                                width: 106*fem,
                                height: 87*fem,
                                child: Image.asset(
                                  'images/upload.png',
                                  fit: BoxFit.cover,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Container(
                        // price4xS (14:35)
                        margin: EdgeInsets.fromLTRB(0*fem, 0*fem, 4*fem, 5*fem),
                        child: Text(
                          'Price',
                          style: SafeGoogleFont (
                            'Inter',
                            fontSize: 22*ffem,
                            fontWeight: FontWeight.w700,
                            height: 0.0125*ffem/fem,
                            letterSpacing: -0.44*fem,
                            color: Color(0xff4f723e),
                          ),
                        ),
                      ),
                      TextField(
                        textAlign: TextAlign.center,
                        controller: _priceController,
                        decoration: InputDecoration(
                          fillColor: Colors.grey.shade100,
                          filled: true,
                          //border: OutlineInputBorder(),
                          hintText: 'Price',
                          border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(10),),
                        ),
                      ),
                      SizedBox(height: 10.0,),


                      ElevatedButton(
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.blueGrey,
                        ),
                        onPressed: () async {
                          var gameName = _gameNameController.text;
                          var description = _descriptionController.text;
                          var price = double.parse(_priceController.text);
                          int category = items.indexOf(dropdownvalue)+1;
                          bool? success = await postGame(gameName, description, category, photo!, apk!, price);
                          if (success != null && success == true) {
                            displayDialog(context, "Oyun yüklendi", "Başarılı");
                          } else {
                            displayDialog(context, "Oyun yüklenemedi", "başarısız");
                          }

                        },
                        child: const Text("Upload"),
                      ),
                      /*
                      Container(
                        // autogroup2zrbnX4 (VjrVXubdMhbVREUXaT2ZRB)
                        margin: EdgeInsets.fromLTRB(80*fem, 0*fem, 80*fem, 0*fem),
                        width: double.infinity,
                        height: 56*fem,
                        decoration: BoxDecoration (
                          color: Color(0xff4f723e),
                          borderRadius: BorderRadius.circular(28*fem),
                        ),
                        child: Center(
                          child: Text(
                            'Upload',
                            style: SafeGoogleFont (
                              'Inter',
                              fontSize: 32*ffem,
                              fontWeight: FontWeight.w700,
                              height: 1.2125*ffem/fem,
                              letterSpacing: -0.64*fem,
                              color: Color(0xffffffff),
                            ),
                          ),
                        ),
                      ),
                      */
                    ],
                  ),
                ),
              ],
            ),
          ),
        ),
            ),
    );
  }

}