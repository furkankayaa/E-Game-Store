import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:deneme_app/utils.dart';

class UserAddAGame extends StatelessWidget {
  const UserAddAGame({super.key});

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
                        //controller: ...,
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
                        //controller: ...,
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
                            Container(
                              // fileuploadimageicon11563229050 (14:34)
                              margin: EdgeInsets.fromLTRB(0*fem, 1*fem, 100*fem, 0*fem),
                              width: 73*fem,
                              height: 72*fem,
                              child: Image.asset(
                                'images/addimg.png',
                                fit: BoxFit.cover,
                              ),
                            ),
                            Container(
                              // upload1A6N (14:38)
                              width: 106*fem,
                              height: 87*fem,
                              child: Image.asset(
                                'images/upload.png',
                                fit: BoxFit.cover,
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
                        //controller: ...,
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