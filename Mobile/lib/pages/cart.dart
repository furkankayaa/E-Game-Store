import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:deneme_app/utils.dart';

class CardData {
  String name = "";
  String price = "";
  String imgPath = "";
  CardData(String name, String price, String imgPath) {
    this.name = name;
    this.price = price;
    this.imgPath = imgPath;
  }
}

String trashPath = "images/trash.png";

class UserCart extends StatelessWidget {
  UserCart({super.key});

  final List<CardData> datas = <CardData>[
    CardData('Clash of Clans', '\$3.99', 'images/clash.png'),
    CardData('Angry Birds', '\$5.99', 'images/angry.png'),
    CardData('Clash Royale', '\$1.99', 'images/royale.jpg'),
    CardData('Candy Crush', '\$2.99', 'images/candy.png')
  ];

  @override
  Widget build(BuildContext context) {
    double baseWidth = 393;
    double fem = MediaQuery.of(context).size.width / baseWidth;
    double ffem = fem * 0.97;
    return Scaffold(
        backgroundColor: Color(0xFFFCFAF8),
        body: Container(
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
                          itemCount: datas.length,
                          itemBuilder: (BuildContext context, int index) {
                            return Container(
                                height: 225,
                                margin: EdgeInsets.all(2),
                                child: Center(
                                  child: _buildCard(
                                      datas[index].name,
                                      datas[index].price,
                                      datas[index].imgPath,
                                      context),
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
                  SizedBox(height: 20.0,)
                ],
              ),
            ),
          ),
        ));
  }

  Widget _buildCard(String name, String price, String imgPath, context) {
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
                        image: AssetImage(imgPath), fit: BoxFit.contain),
                  ),
                ),
              ),
              SizedBox(height: 7.0),
              Text(price,
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
                child: Container(
                  height: 40.0,
                  width: 40.0,
                  decoration: BoxDecoration(
                    image: DecorationImage(
                        image: AssetImage(trashPath), fit: BoxFit.contain),
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
