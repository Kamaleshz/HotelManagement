import React, { useState } from 'react';
import './Login.css';
import UserManagementService from '../../Services/UserManagementApiCalls';
import { ToastContainer, toast } from 'react-toastify';

function LoginPopup() {
  const [show, setShow] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [emailValid, setEmailValid] = useState(false);
  const [passwordValid, setPasswordValid] = useState(false);
  const [emailClicked, setEmailClicked] = useState(false);
  const [passwordClicked, setPasswordClicked] = useState(false);

  const handleEmailChange = (e) => {
    const newEmail = e.target.value;
    setEmail(newEmail);
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const isValidFormat = emailRegex.test(newEmail);
    setEmailValid(isValidFormat && newEmail.includes('@') && newEmail.split('@')[1].includes('.'));
  };
  
  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    setPasswordValid(newPassword.length > 0);
  };

  const login = async () => {
    try {
        await UserManagementService.login({ userEmail: email, password: password });
        toast.success("Logged In successfully")
        handleClose();
    } catch (error) {
        toast.error(error.response.data.error);
    }
  };

  const handleClose = () => {
    setShow(false);
  };

  const handleShow = () => {
    setShow(true);
  };

  const isFormValid = emailValid && passwordValid;

  return (
    <>
      <button className="btn btn-primary" onClick={handleShow}>
        Login
      </button>

      {show && (
        <div className="modal" tabIndex="-1" role="dialog">
          <div className="modal-dialog" role="document">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Login</h5>
                <button type="button" className="close" data-dismiss="modal" aria-label="Close" onClick={handleClose}>
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div className="modal-body">
                <form>
                  <div className="form-group">
                    <label htmlFor="exampleInputEmail1">Email address</label>
                    <input 
                      type="email" 
                      className={`form-control ${!emailValid && emailClicked ? 'invalid' : ''}`}
                      id="Email1"
                      aria-describedby="emailHelp"
                      placeholder="Enter email"
                      value={email}
                      onChange={handleEmailChange}
                      onClick={() => setEmailClicked(true)}
                    />
                    <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
                  </div>
                  <div className="form-group">
                    <label htmlFor="exampleInputPassword1">Password</label>
                    <input
                      type="password"
                      className={`form-control ${!passwordValid && passwordClicked ? 'invalid' : ''}`}
                      id="Password"
                      placeholder="Password"
                      value={password}
                      onChange={handlePasswordChange}
                      onClick={() => setPasswordClicked(true)}
                    />
                  </div>
                </form>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary" onClick={handleClose}>Close</button>
                <button type="button" className="btn btn-primary" onClick={login} disabled={!isFormValid}>Login</button>
              </div>
            </div>
          </div>
        </div>
      )}
      <ToastContainer />
    </>
  );
}

export default LoginPopup;
