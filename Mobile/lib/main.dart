import 'package:deneme_app/pages/sign_in.dart';
import 'package:deneme_app/pages/sign_up.dart';
import 'package:deneme_app/pages/navbar.dart';
import 'package:flutter/material.dart';

/*
Future main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp();

  runApp(MyApp());
}
*/

void main() {
  runApp(const MyApp());
}


class MyApp extends StatelessWidget {
  const MyApp({super.key});
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const SignIn(),
      debugShowCheckedModeBanner: false,
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.green,
      ),
    );
  }
}