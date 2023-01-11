import 'package:deneme_app/pages/homepage.dart';
import 'package:flutter/material.dart';
import 'package:deneme_app/pages/navbar.dart';
import 'package:deneme_app/pages/sign_in.dart';
import 'package:deneme_app/pages/sign_up.dart';
import 'package:google_fonts/google_fonts.dart';
import 'dart:convert' show ascii, base64, json, jsonEncode, jsonDecode;
import 'package:deneme_app/utils.dart';
import 'package:http/http.dart' as http;

class TokenInfo {
  final String token;
  final String expiration;
  TokenInfo({
    required this.token,
    required this.expiration,
  });

  factory TokenInfo.fromJson(Map<String, dynamic> json) {
    return TokenInfo(
      token: json['token'],
      expiration: json['expireDate']
    );
  }
}

class AccessToken{
  final bool isSuccess;
  final String message;
  final TokenInfo token;
  AccessToken({
    required this.isSuccess,
    required this.message,
    required this.token,
  });

  factory AccessToken.fromJson(Map<String, dynamic> json) {
    return AccessToken(
      isSuccess: json["isSuccess"], 
      message: json["message"],
      token: TokenInfo.fromJson(json["tokenInfo"])
    );
  }
}


class SignIn extends StatelessWidget {
  const SignIn({super.key});
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Body(),
      ),
    );
  }
}

class Body extends StatelessWidget {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  void displayDialog(context, title, text) => showDialog(
        context: context,
        builder: (context) =>
            AlertDialog(title: Text(title), content: Text(text)),
  );

  Future<String?> attemptLogIn(String email, String password) async {
    //final uri = Uri.parse("http://10.0.2.2:5000/api/Auth/UserLogin");
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Auth/UserLogin");
    
    var res = await http.post(
      uri,
      headers: {
        "Accept": "application/json",
        "content-type": "application/json"
      },
      body: jsonEncode({'email': email, 'password': password}),
    );
    if (res.statusCode == 200){
      var result = AccessToken.fromJson(jsonDecode(res.body));
      return result.token.token;
  
      } 
    return null;
  }

  @override
  Widget build(BuildContext context) {
    double baseWidth = 393;
    double fem = MediaQuery.of(context).size.width / baseWidth;
    double ffem = fem * 0.97;
    return Scaffold(
        body: SingleChildScrollView(
      child: Center(
        child: Column(
          children: [
            const SizedBox(height: 60.0),
            Image.asset("images/pp.png", height: 250, width: 250,),
            const SizedBox(height: 50.0),
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Container(
                  // nameRWr (14:17)
                  margin:
                      EdgeInsets.fromLTRB(0 * fem, 0 * fem, 210 * fem, 8 * fem),
                  child: Text(
                    'E Mail',
                    style: SafeGoogleFont(
                      'Inter',
                      fontSize: 22 * ffem,
                      fontWeight: FontWeight.w700,
                      height: 0.2125 * ffem / fem,
                      letterSpacing: -0.44 * fem,
                      color: Color(0xff4f723e),
                    ),
                  ),
                ),
                TextField(
                  textAlign: TextAlign.center,
                  controller: _emailController,
                  decoration: InputDecoration(
                    fillColor: Colors.grey.shade100,
                    filled: true,
                    //border: OutlineInputBorder(),
                    hintText: 'E Mail',
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                ),
                SizedBox(height: 40.0,),
                Container(
                  // nameRWr (14:17)
                  margin:
                      EdgeInsets.fromLTRB(0 * fem, 0 * fem, 210 * fem, 8 * fem),
                  child: Text(
                    'Password',
                    style: SafeGoogleFont(
                      'Inter',
                      fontSize: 22 * ffem,
                      fontWeight: FontWeight.w700,
                      height: 0.2125 * ffem / fem,
                      letterSpacing: -0.44 * fem,
                      color: Color(0xff4f723e),
                    ),
                  ),
                ),
                TextField(
                  obscureText: true,
                  textAlign: TextAlign.center,
                  controller: _passwordController,
                  decoration: InputDecoration(
                    fillColor: Colors.grey.shade100,
                    filled: true,
                    //border: OutlineInputBorder(),
                    hintText: 'Password',
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                ),
                SizedBox(height: 40.0,),
                ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.blueGrey,
                  ),
                  onPressed: () async {

                      var username = _emailController.text;
                      var password = _passwordController.text;
                      
                      var token = await attemptLogIn(username, password);
                      if (token != null) {
                        storage.write(key: "token", value: token);
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                                //builder: (context) => HomePage.fromBase64(jwt)
                                builder: (context) =>  NavBar(),
                                ),
                        );
                      } else {
                          displayDialog(context, "An Error Occurred", "hata oluştu");
                      }

                    /*
                    Navigator.of(context).push(
                      MaterialPageRoute(builder: (context) {
                        return const NavBar();
                      }),
                    );
                  */
                  },
                  child: const Text("Sign In"),
                ),
                SizedBox(height: 80.0,),
                Container(
                  // nameRWr (14:17)
                  margin:
                      EdgeInsets.fromLTRB(0 * fem, 0 * fem, 0 * fem, 12 * fem),
                  child: Text(
                    'Already have an account?',
                    style: SafeGoogleFont(
                      'Inter',
                      fontSize: 22 * ffem,
                      fontWeight: FontWeight.w700,
                      height: 0.2125 * ffem / fem,
                      letterSpacing: -0.44 * fem,
                      color: Color(0xff4f723e),
                    ),
                  ),
                ),
                ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.blueAccent,
                  ),
                  onPressed: () {
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (context) => SignUp(
                          ),
                        ),
                      );
                      },
                  child: const Text("Sign Up"),
                ),
              ],
            ),
          ],
        ),
      ),
    ),
    );
  }
}