import React from "react";
import "./App.css";
import "./styles/global.css";
import { Routes } from "./routes/config";
import { GlobalStyles } from "./styles/index";
import { AuthProvider } from "./authentication/use-auth";
import toast, { Toaster } from "react-hot-toast";
//This is almost the base level of your app.  You can also put global things here.
function App() {
  return (
    <div className="App">
      <GlobalStyles />
      <Toaster />
      <AuthProvider>
        <Routes />
      </AuthProvider>
    </div>
  );
}

export default App;

/*Contents of the original return of fucntion App()*/
// <div className="App">
//   <GlobalStyles />
//   <AuthProvider>
//     <Routes />
//   </AuthProvider>
// </div>
