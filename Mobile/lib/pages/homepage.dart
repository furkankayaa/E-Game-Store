import 'package:flutter/material.dart';
import 'package:deneme_app/pages/game_details.dart';

class CardData{
  String name = "";
  String price = ""; 
  String rating = ""; 
  String description = "";
  String category = "";
  String imgPath = "";
  CardData(String name,String price,String rating,String description,String category,String imgPath){
    this.name = name;
    this.price = price;
    this.rating = rating;
    this.description = description;
    this.category = category;
    this.imgPath = imgPath;
  }
}



class HomePage extends StatelessWidget {
  HomePage({super.key});

  final List<CardData> datas = <CardData>[CardData('Clash of Clans','\$3.99', '4.7','web based strategy game','Strategy','images/clash.png'),
                                          CardData('Angry Birds', '\$5.99', '4.3', 'local strategy game', 'Strategy','images/angry.png'),
                                          CardData('Clash Royale','\$1.99','4.4','web based strategy game','Strategy','images/royale.jpg'),
                                          CardData('Candy Crush', '\$2.99','4.8','puzzle game','Puzzle','images/candy.png')];


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Color(0xFFFCFAF8),
      body: Container(
        padding: EdgeInsets.only(right: 10.0,left: 40.0, top: 50.0, bottom: 20.0),
        width: MediaQuery.of(context).size.width - 30.0,
        height: MediaQuery.of(context).size.height - 50.0,
        child: ListView.builder(
          padding: EdgeInsets.only(right: 15.0,left: 15.0),
          itemCount: datas.length,
          itemBuilder: (BuildContext context, int index) {
            return Container(

              height: 225,
              margin: EdgeInsets.all(2),
              child: Center(
                  child: _buildCard(datas[index].name, datas[index].price, datas[index].rating, datas[index].description, datas[index].category, datas[index].imgPath,context),
                )
              );
          }
        )
      )

      

      /*
      ListView(
        children: <Widget>[
          SizedBox(height: 30.0),
          Container(
              padding: EdgeInsets.only(right: 15.0,left: 15.0),
              width: MediaQuery.of(context).size.width - 30.0,
              height: MediaQuery.of(context).size.height - 50.0,
              child: GridView.count(
                crossAxisCount: 2,
                primary: false,
                crossAxisSpacing: 10.0,
                mainAxisSpacing: 15.0,
                childAspectRatio: 0.8,
                children: <Widget>[
                  _buildCard('Clash of Clans','\$3.99', '4.7','web based strategy game','Strategy','images/clash.png',context),
                  _buildCard('Angry Birds', '\$5.99', '4.3', 'local strategy game', 'Strategy','images/angry.png',context),
                  _buildCard('Clash Royale','\$1.99','4.4','web based strategy game','Strategy','images/royale.jpg',context),
                  _buildCard('Candy Crush', '\$2.99','4.8','puzzle game','Puzzle','images/candy.png',context)
                ],
              )),
          SizedBox(height: 15.0)
        ],
      ),
      */
    );
  }

  Widget _buildCard(String name, String price, String rating, String description,
      String category, String imgPath, context) {
    return Padding(
      padding: EdgeInsets.only(top: 5.0, bottom: 5.0, left: 5.0, right: 5.0),
      child: InkWell(
        onTap: () {
          Navigator.of(context).push(
            MaterialPageRoute(
              builder: (context) => GameDetail(
                assetPath: imgPath,
                gamePrice: price,
                gameName: name,
                gameDescription: description,
                gameCategory: category,
              ),
            ),
          );
        },
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
                  child: Container(color: Color(0xFFEBEBEB), height: 1.0)),
              Padding(
                padding: EdgeInsets.only(left: 5.0, right: 5.0),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    Text(category,
                      style: TextStyle(
                        color: Color(0xFFCC8053),
                        fontFamily: 'Varela',
                        fontSize: 14.0)),
                    SizedBox(height: 5),
                    Text(rating,
                      style: TextStyle(
                        color: Color(0xFFCC8053),
                        fontFamily: 'Varela',
                        fontSize: 14.0)),
                  ],
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
