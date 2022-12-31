import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:deneme_app/utils.dart';
import 'package:deneme_app/pages/sign_in.dart';

class UserProfile extends StatelessWidget {
  const UserProfile({super.key});

  @override
  Widget build(BuildContext context) {
    double baseWidth = 393;
    double fem = MediaQuery.of(context).size.width / baseWidth;
    double ffem = fem * 0.97;
    return Scaffold(
      //appBar: AppBar(title: const Text("AAAAA"),),
      body: Container(
        // iphone14pro6Xpb (8:12)
        width: double.infinity,
        decoration: BoxDecoration(
          color: Color(0xffc7e3d1),
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Container(
              // autogroupctmzPbu (Y7qMc3Zcp5tWNb1pPscTmZ)
              padding:
                  EdgeInsets.fromLTRB(65 * fem, 74 * fem, 64 * fem, 42 * fem),
              width: double.infinity,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  InkWell(
                    onTap: () {
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (context) => SignIn(
                          ),
                        ),
                      );
                    },
                    child: Container(
                      // autogroup8c31JD5 (Y7qLkKVUQ8byA7gYG38C31)
                      margin: EdgeInsets.fromLTRB(
                          0 * fem, 0 * fem, 0 * fem, 15 * fem),
                      padding: EdgeInsets.fromLTRB(
                          24 * fem, 15 * fem, 12 * fem, 19 * fem),
                      width: double.infinity,
                      decoration: BoxDecoration(
                        color: Color(0xff4f723e),
                        borderRadius: BorderRadius.circular(25 * fem),
                      ),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Container(
                            // info215093812801PEX (11:3)
                            margin: EdgeInsets.fromLTRB(
                                0 * fem, 0 * fem, 14 * fem, 0 * fem),
                            width: 32 * fem,
                            height: 35 * fem,
                            child: const Image(
                                image: AssetImage('images/info.png'),
                                fit: BoxFit.cover),
                          ),
                          Container(
                            // profileinformationWa3 (8:21)
                            margin: EdgeInsets.fromLTRB(
                                0 * fem, 4 * fem, 0 * fem, 0 * fem),
                            child: Text(
                              'Profile Information',
                              style: SafeGoogleFont(
                                'Inter',
                                fontSize: 20 * ffem,
                                fontWeight: FontWeight.w700,
                                height: 1.2125 * ffem / fem,
                                color: Color(0xffffffff),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                  
                  InkWell(
                    onTap: (){
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (context) => SignIn(
                          ),
                        ),
                      );
                    },
                    child: Container(
                      // autogroupv4l7aLw (Y7qLxp8exxtNA3UUjev4L7)
                      margin: EdgeInsets.fromLTRB(
                          0 * fem, 0 * fem, 0 * fem, 15 * fem),
                      padding: EdgeInsets.fromLTRB(
                          22 * fem, 17 * fem, 81 * fem, 17 * fem),
                      width: double.infinity,
                      decoration: BoxDecoration(
                        color: Color(0xff4f723e),
                        borderRadius: BorderRadius.circular(25 * fem),
                      ),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Container(
                            // images2Ux7 (13:9)
                            margin: EdgeInsets.fromLTRB(
                                0 * fem, 0 * fem, 56 * fem, 0 * fem),
                            width: 35 * fem,
                            height: 35 * fem,
                            child: const Image(
                                image: AssetImage('images/lib.png'),
                                fit: BoxFit.cover),
                          ),
                          Text(
                            // librarypWB (9:24)
                            'Library',
                            style: SafeGoogleFont(
                              'Inter',
                              fontSize: 20 * ffem,
                              fontWeight: FontWeight.w700,
                              height: 1.2125 * ffem / fem,
                              color: Color(0xffffffff),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                  InkWell(
                    onTap: (){
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (context) => SignIn(
                          ),
                        ),
                      );
                    },
                    child: Container(
                      // autogroupucskNGo (Y7qM4UdtGAinW7kfWeuCSK)
                      margin: EdgeInsets.fromLTRB(
                          0 * fem, 0 * fem, 0 * fem, 286 * fem),
                      padding: EdgeInsets.fromLTRB(
                          17 * fem, 13 * fem, 23 * fem, 14 * fem),
                      width: double.infinity,
                      decoration: BoxDecoration(
                        color: Color(0xff4f723e),
                        borderRadius: BorderRadius.circular(25 * fem),
                      ),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Container(
                            // 6Co (13:8)
                            margin: EdgeInsets.fromLTRB(
                                0 * fem, 0 * fem, 8 * fem, 0 * fem),
                            width: 45 * fem,
                            height: 42 * fem,
                            child: const Image(
                                image: AssetImage('images/published.png'),
                                fit: BoxFit.cover),
                          ),
                          Container(
                            // publishedgamesdTd (9:25)
                            margin: EdgeInsets.fromLTRB(
                                0 * fem, 1 * fem, 0 * fem, 0 * fem),
                            child: Text(
                              'Published Games',
                              style: SafeGoogleFont(
                                'Inter',
                                fontSize: 20 * ffem,
                                fontWeight: FontWeight.w700,
                                height: 1.2125 * ffem / fem,
                                color: Color(0xffffffff),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                  InkWell(
                    onTap: (){
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (context) => SignIn(
                          ),
                        ),
                      );
                    },
                    child: Container(
                      // autogroupdk6tATZ (Y7qMUP7ihsHhnc3oTpDk6T)
                      margin: EdgeInsets.fromLTRB(
                          15 * fem, 0 * fem, 16 * fem, 0 * fem),
                      padding: EdgeInsets.fromLTRB(
                          13 * fem, 0 * fem, 38 * fem, 0 * fem),
                      width: double.infinity,
                      height: 57 * fem,
                      decoration: BoxDecoration(
                        color: Color(0xffb8cab0),
                        borderRadius: BorderRadius.circular(25 * fem),
                      ),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Container(
                            // background1rbH (13:10)
                            margin: EdgeInsets.fromLTRB(
                                0 * fem, 0 * fem, 15 * fem, 5 * fem),
                            width: 49 * fem,
                            height: 52 * fem,
                            child: const Image(
                                image: AssetImage('images/out.png'),
                                fit: BoxFit.cover),
                          ),
                          Container(
                            // logoutmCT (8:22)
                            margin: EdgeInsets.fromLTRB(
                                0 * fem, 0 * fem, 0 * fem, 4 * fem),
                            child: Text(
                              'Log out',
                              style: SafeGoogleFont(
                                'Inter',
                                fontSize: 32 * ffem,
                                fontWeight: FontWeight.w700,
                                height: 1.2125 * ffem / fem,
                                color: Color(0xffffffff),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
