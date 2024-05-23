import { ToastContainer } from 'react-toastify';
import './App.css';
import LoginPopup from './Features/UserManagement/LogIn/LogIn';
import SignUp from './Features/UserManagement/SignUp/SignUp';
import UserProfile from './Features/UserManagement/UserProfile/UserProfile';
import {
  BrowserRouter as Router,
  Route,
  Routes,
} from "react-router-dom";
function App() {
  return (
    <Router>
      <div className="App">
      <ToastContainer />
        <Routes> 
          <Route path="/login" element={<LoginPopup/>}/>
          <Route path="/signup" element={<SignUp/>}/>
          <Route path="/userprofile" element={<UserProfile/>}/>
        </Routes>
      </div>
    </Router>
  );
}

export default App;
