import React, { useState } from 'react';
import './SignUp.css';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { ToastContainer, toast } from 'react-toastify';

function SignUp() {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [firstNameValid, setFirstNameValid] = useState(false);
  const [lastNameValid, setLastNameValid] = useState(false);
  const [emailValid, setEmailValid] = useState(false);
  const [passwordValid, setPasswordValid] = useState(false);
  const [phoneNumberValid, setPhoneNumberValid] = useState(false);
  const [firstNameTouched, setFirstNameTouched] = useState(false);
  const [lastNameTouched, setLastNameTouched] = useState(false);
  const [emailTouched, setEmailTouched] = useState(false);
  const [passwordTouched, setPasswordTouched] = useState(false);
  const [phoneNumberTouched, setPhoneNumberTouched] = useState(false);
  const [passwordVisible, setPasswordVisible] = useState(false);

  const handleFirstNameChange = (e) => {
    const newFirstName = e.target.value;
    setFirstName(newFirstName);
    setFirstNameValid(newFirstName.length > 0);
  };

  const handleFirstNameBlur = () => {
    setFirstNameTouched(true);
  };

  const handleLastNameChange = (e) => {
    const newLastName = e.target.value;
    setLastName(newLastName);
    setLastNameValid(newLastName.length > 0);
  };

  const handleLastNameBlur = () => {
    setLastNameTouched(true);
  };

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

  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,15}$/;
    const isValidFormat = passwordRegex.test(newPassword);
    setPasswordValid(isValidFormat);
  };

  const handlePasswordBlur = () => {
    setPasswordTouched(true);
  };

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };

  const handlePhoneNumberChange = (e) => {
    const newPhoneNumber = e.target.value;
    setPhoneNumber(newPhoneNumber);
    const isValidFormat = /^\d{10}$/.test(newPhoneNumber);
    setPhoneNumberValid(isValidFormat);
  };

  const handlePhoneNumberBlur = () => {
    setPhoneNumberTouched(true);
  };

  const signIn = async () => {
    try {
      await UserManagementService.register({ 
        firstName: firstName,
        lastName: lastName,
        userEmail: email, 
        password: password,
        userPhoneNumber: phoneNumber,
        userRole: 3 
      });
      toast.success("Signed Up successfully")
    } catch (error) {
      toast.error(error.response.data.error);
    }
  };

  const isFormValid = firstNameValid && lastNameValid && emailValid && passwordValid && phoneNumberValid;

  return (
    <div className="container">
      <h2>Sign Up</h2>
      <form>
      <div className="row">
          <div className="col-sm-6">
            <div className="form-group">
              <label htmlFor="inputFirstName">First Name</label>
              <input
                type="text"
                className={`form-control ${!firstNameValid && firstNameTouched ? 'is-invalid' : ''}`}
                id="inputFirstName"
                placeholder="Enter first name"
                value={firstName}
                onChange={handleFirstNameChange}
                onBlur={handleFirstNameBlur}
              />
              {!firstNameValid && firstNameTouched && (
                <small className="form-text text-danger">Please enter your first name.</small>
              )}
            </div>
          </div>
          <div className="col-sm-6">
            <div className="form-group">
              <label htmlFor="inputLastName">Last Name</label>
              <input
                type="text"
                className={`form-control ${!lastNameValid && lastNameTouched ? 'is-invalid' : ''}`}
                id="inputLastName"
                placeholder="Enter last name"
                value={lastName}
                onChange={handleLastNameChange}
                onBlur={handleLastNameBlur}
              />
              {!lastNameValid && lastNameTouched && (
                <small className="form-text text-danger">Please enter your last name.</small>
              )}
            </div>
          </div>
        </div>
        <div className="form-group">
          <label htmlFor="inputEmail">Email address</label>
          <input
            type="email"
            className={`form-control ${!emailValid && emailTouched ? 'is-invalid' : ''}`}
            id="inputEmail"
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
          <label htmlFor="inputPassword">Password</label>
          <div className="password-input-container">
            <input
              type={passwordVisible ? 'text' : 'password'}
              className={`form-control ${!passwordValid && passwordTouched ? 'is-invalid' : ''}`}
              id="inputPassword"
              placeholder="Password"
              value={password}
              onChange={handlePasswordChange}
              onBlur={handlePasswordBlur}
            />
            <button
              type="button"
              className={`password-toggle-btn ${!passwordValid && passwordTouched ? 'invalid': `` }`}
              onClick={togglePasswordVisibility}
            >
              {passwordVisible ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye-fill"></i>}
            </button>
          </div>
          {!passwordValid && passwordTouched && (
            <small className="form-text text-danger">Password must be between 8 and 15 characters, and contain at least one uppercase letter, one lowercase letter, one number, and one special character.</small>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="inputPhoneNumber">Phone Number</label>
          <input
            type="tel"
            className={`form-control ${!phoneNumberValid && phoneNumberTouched ? 'is-invalid' : ''}`}
            id="inputPhoneNumber"
            placeholder="Enter phone number"
            value={phoneNumber}
            onChange={handlePhoneNumberChange}
            onBlur={handlePhoneNumberBlur}
          />
          {!phoneNumberValid && phoneNumberTouched && (
            <small className="form-text text-danger">Please enter a valid phone number.</small>
          )}
        </div>
        <button type="button" className="signin-btn" onClick={signIn} disabled={!isFormValid}>Sign In</button>
      </form>
      <ToastContainer />
    </div>
  );
}

export default SignUp;
