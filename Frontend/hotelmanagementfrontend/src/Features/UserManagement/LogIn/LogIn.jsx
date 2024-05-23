import React, { useState } from 'react';
import './Login.css';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import {useDispatch} from 'react-redux';
import { setUserDetails } from '../../State/UserDetails.actions';

function LoginPopup() {
  const [show, setShow] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [emailValid, setEmailValid] = useState(false);
  const [passwordValid, setPasswordValid] = useState(false);
  const [emailTouched, setEmailTouched] = useState(false);
  const [passwordTouched, setPasswordTouched] = useState(false);
  const [passwordVisible, setPasswordVisible] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleEmailChange = (e) => {
    const newEmail = e.target.value;
    setEmail(newEmail);
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]{2,}$/;
    const isValidFormat = emailRegex.test(newEmail);
    setEmailValid(isValidFormat);
  };

  const handleEmailBlur = () => {
    setEmailTouched(true);
  };

  const handlePasswordBlur = () => {
    setPasswordTouched(true);
  }

  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    setPasswordValid(newPassword.length > 0);
  };

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  }

  const login = async () => {
    try {
      const response = await UserManagementService.login({ userEmail: email, password: password });
        if(response.success)
          {
            toast.success("Logged In successfully")
            console.log("Response.data",response.data);
            dispatch(setUserDetails(response.data));
            navigate("/userprofile");
            handleClose();
          }
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
                      maxLength={30}
                      onChange={handleEmailChange}
                      onBlur={handleEmailBlur}
                    />
                    {!emailValid && emailTouched && (
                      <small className="form-text text-danger">Please enter a valid email address.</small>
                    )}
                  </div>

                  <div className="form-group">
                    <label htmlFor="inputPassword">Password</label>
                    <div className='login-password-input-container'>
                    <input
                      type={passwordVisible ? 'text' : 'password'}
                      className={`form-control ${!passwordValid && passwordTouched ? 'is-invalid' : ''}`}
                      id="Password" 
                      placeholder="Password"
                      value={password}
                      maxLength={15}
                      onChange={handlePasswordChange}
                      onBlur={handlePasswordBlur}
                    />
                    <button
                      type="button"
                      className={`login-password-toggle-btn ${!passwordValid && passwordTouched ? 'invalid': `` }`}
                      onClick={togglePasswordVisibility}
                    >
              {passwordVisible ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye-fill"></i>}
            </button>
                    { !passwordValid && passwordTouched && (
                      <small className='form-text text-danger'>Please enter the password.</small>
                    )}
                  </div>
                  </div>
                </form>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary" onClick={handleClose}>Close</button>
                <button type="button" className="login-btn" onClick={login} disabled={!isFormValid}>Login</button>  
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default LoginPopup;
