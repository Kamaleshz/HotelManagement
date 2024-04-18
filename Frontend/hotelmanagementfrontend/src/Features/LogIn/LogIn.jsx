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
  const [emailTouched, setEmailTouched] = useState(false);
  const [passwordTouched, setPasswordTouched] = useState(false);

  const handleEmailChange = (e) => {
    const newEmail = e.target.value;
    setEmail(newEmail);
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]{2,}$/;
    const isValidFormat = emailRegex.test(newEmail);
    setEmailValid(isValidFormat);
  };

  const handleEmailBlur = () => {
    setEmailTouched(true); // Set emailTouched to true when email field is blurred
  };

  const handlePasswordBlur = () => {
    setPasswordTouched(true);
  }

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
              </div>
              <div className="modal-body">
                <form>
                  <div className="form-group">
                    <label htmlFor="exampleInputEmail1">Email address</label>
                    <input
                      type="email"
                      className={`form-control ${!emailValid && emailTouched ? 'is-invalid' : ''}`}
                      id="Email1"
                      aria-describedby="emailHelp"
                      placeholder="Enter email"
                      value={email}
                      onChange={handleEmailChange}
                      onBlur={handleEmailBlur}
                    />
                    {!emailValid && emailTouched && (
                      <small className="form-text text-danger">Please enter a valid email address.</small>
                    )}
                  </div>

                  <div className="form-group">
                    <label htmlFor="exampleInputPassword1">Password</label>
                    <input
                      type="password"
                      className={`form-control ${!passwordValid && passwordTouched ? 'is-invalid' : ''}`}
                      id="Password"
                      placeholder="Password"
                      value={password}
                      onChange={handlePasswordChange}
                      onBlur={handlePasswordBlur}
                    />
                    { !passwordValid && passwordTouched && (
                      <small className='form-text text-danger'>Please enter the password.</small>
                    )}
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
