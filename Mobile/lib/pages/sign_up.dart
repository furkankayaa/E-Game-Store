import 'package:flutter/material.dart';
import 'package:deneme_app/pages/navbar.dart';
import 'package:deneme_app/pages/sign_in.dart';
import 'package:deneme_app/pages/sign_up.dart';
import 'dart:convert' show ascii, base64, json, jsonEncode, jsonDecode;
import 'package:deneme_app/utils.dart';
import 'package:http/http.dart' as http;


class AccessToken{
  final bool isSuccess;
  final String message;
  final String? tokenInfo;
  AccessToken({
    required this.isSuccess,
    required this.message,
    required this.tokenInfo,
  });

  factory AccessToken.fromJson(Map<String, dynamic> json) {
    return AccessToken(
      isSuccess: json["isSuccess"], 
      message: json["message"],
      tokenInfo: json["tokenInfo"]
    );
  }
}


class SignUp extends StatelessWidget {
  const SignUp({super.key});
  
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
  final TextEditingController _nameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _passwordConfirmController = TextEditingController();

  void displayDialog(context, title, text) => showDialog(
        context: context,
        builder: (context) =>
            AlertDialog(title: Text(title), content: Text(text)),
  );

  Future<bool?> attemptRegister(String fullName, String email, String password, String passwordConfirm) async {
    //final uri = Uri.parse("http://10.0.2.2:5000/api/Auth/Register");
    final uri = Uri.parse("https://e-gamestore.onrender.com/api/Auth/Register");
    var res = await http.post(
      uri,
      headers: {
        "Accept": "application/json",
        "content-type": "application/json"
      },
      body: jsonEncode({'fullName': fullName, 'email': email, 'password': password, 'confirmPassword': passwordConfirm}),
    );
    if (res.statusCode == 200){
      var result = AccessToken.fromJson(jsonDecode(res.body));
      return result.isSuccess;
  
      } 
    return false;
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
            const SizedBox(height: 100.0),
            
            const SizedBox(height: 50.0),
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Container(
                  // nameRWr (14:17)
                  margin:
                      EdgeInsets.fromLTRB(0 * fem, 0 * fem, 210 * fem, 8 * fem),
                  child: Text(
                    'Full Name',
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
                  controller: _nameController,
                  decoration: InputDecoration(
                    fillColor: Colors.grey.shade100,
                    filled: true,
                    //border: OutlineInputBorder(),
                    hintText: 'Full Name',
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
                Container(
                  // nameRWr (14:17)
                  margin:
                      EdgeInsets.fromLTRB(0 * fem, 0 * fem, 130 * fem, 8 * fem),
                  child: Text(
                    'Password Confirm',
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
                  controller: _passwordConfirmController,
                  decoration: InputDecoration(
                    fillColor: Colors.grey.shade100,
                    filled: true,
                    //border: OutlineInputBorder(),
                    hintText: 'Password Confirm',
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

                      var fullName = _nameController.text;
                      var username = _emailController.text;
                      var password = _passwordController.text;
                      var passwordConfirm = _passwordConfirmController.text;
                      
                      if (password == passwordConfirm){
                        var success = await attemptRegister(fullName, username, password, passwordConfirm);
                        if (success == true) {
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                                //builder: (context) => HomePage.fromBase64(jwt)
                                builder: (context) =>  SignIn(),
                                ),
                        );
                        } else {
                          displayDialog(context, "An Error Occurred", "hata oluştu");
                        }
                      }
                      else{
                        displayDialog(context, "Parolalar eşit değil", "hata oluştu");
                      }

                    /*
                    Navigator.of(context).push(
                      MaterialPageRoute(builder: (context) {
                        return const NavBar();
                      }),
                    );
                  */
                  },
                  child: const Text("Sign Up"),
                ),
                SizedBox(height: 80.0,),
              ],
            ),
          ],
        ),
      ),
    ),
    );
  }

}