import React, { useState } from 'react';
import './SignUp.css';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setUserDetails } from '../../State/UserDetails.actions'; 
import { regExp } from '../Shared/RegExp';

function SignUp() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    userEmail: '',
    userPassword: '',
    userPhoneNumber: ''
  });

  const [validity, setValidity] = useState({
    firstName: false,
    lastName: false,
    userEmail: false,
    userPassword: false,
    userPhoneNumber: false
  });

  const [touched, setTouched] = useState({
    firstName: false,
    lastName: false,
    userEmail: false,
    userPassword: false,
    userPhoneNumber: false
  });

  const [passwordVisible, setPasswordVisible] = useState(false);
  
  const regexp = regExp();
  
  const validate = {
    firstName: (name) => name.length > 0,
    lastName: (name) => name.length > 0,
    userEmail: (email) => regexp.EMAIL_REGEX.test(email),
    userPassword: (password) => regexp.PASSWORD_REGEX.test(password),
    userPhoneNumber: (number) => regexp.PHONE_REGEX.test(number)
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevState) => ({ ...prevState, [name]: value }));
    setValidity((prevState) => ({ ...prevState, [name]: validate[name](value) }));
  };

  const handleBlur = (e) => {
    const { name } = e.target;
    setTouched((prevState) => ({ ...prevState, [name]: true }));
  };

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };
  
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const signIn = async () => {
    try {
      const responce = await UserManagementService.register({
        firstName: formData.firstName,
        lastName: formData.lastName,
        userEmail: formData.userEmail,
        Password: formData.userPassword,
        userPhoneNumber: formData.userPhoneNumber,
        userRole: 3
      });
      toast.success("Signed Up successfully");
      dispatch(setUserDetails(responce.data));
      navigate("/userprofile");
    } catch (error) {
      toast.error(error.response.data.error);
    }
  };

  const isFormValid = Object.values(validity).every(Boolean);

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
                className={`form-control ${!validity.firstName && touched.firstName ? 'is-invalid' : ''}`}
                id="inputFirstName"
                name="firstName"
                placeholder="Enter first name"
                value={formData.firstName}
                maxLength={15}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {!validity.firstName && touched.firstName && (
                <small className="form-text text-danger">Please enter your first name.</small>
              )}
            </div>
          </div>
          <div className="col-sm-6">
            <div className="form-group">
              <label htmlFor="inputLastName">Last Name</label>
              <input
                type="text"
                className={`form-control ${!validity.lastName && touched.lastName ? 'is-invalid' : ''}`}
                id="inputLastName"
                name="lastName"
                placeholder="Enter last name"
                value={formData.lastName}
                maxLength={15}
                onChange={handleChange}
                onBlur={handleBlur}
              />
              {!validity.lastName && touched.lastName && (
                <small className="form-text text-danger">Please enter your last name.</small>
              )}
            </div>
          </div>
        </div>
        <div className="form-group">
          <label htmlFor="inputEmail">Email address</label>
          <input
            type="email"
            className={`form-control ${!validity.userEmail && touched.userEmail ? 'is-invalid' : ''}`}
            id="inputEmail"
            name="userEmail"
            placeholder="Enter email"
            value={formData.userEmail}
            maxLength={30}
            onChange={handleChange}
            onBlur={handleBlur}
          />
          {!validity.userEmail && touched.userEmail && (
            <small className="form-text text-danger">Please enter a valid email address.</small>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="inputPassword">Password</label>
          <div className="password-input-container">
            <input
              type={passwordVisible ? 'text' : 'password'}
              className={`form-control ${!validity.userPassword && touched.userPassword ? 'is-invalid' : ''}`}
              id="inputPassword"
              name="userPassword"
              placeholder="Password"
              value={formData.userPassword}
              maxLength={15}
              onChange={handleChange}
              onBlur={handleBlur}
            />
            <button
              type="button"
              className={`password-toggle-btn ${!validity.userPassword && touched.userPassword ? 'invalid' : ''}`}
              onClick={togglePasswordVisibility}
            >
              {passwordVisible ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye-fill"></i>}
            </button>
          </div>
          {!validity.userPassword && touched.userPassword && (
            <small className="form-text text-danger">
              Password must be between 8 and 15 characters, and contain at least one uppercase letter, one lowercase letter, one number, and one special character.
            </small>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="inputPhoneNumber">Phone Number</label>
          <input
            type="tel"
            className={`form-control ${!validity.userPhoneNumber && touched.userPhoneNumber ? 'is-invalid' : ''}`}
            id="inputPhoneNumber"
            name="userPhoneNumber"
            placeholder="Enter phone number"
            value={formData.userPhoneNumber}
            maxLength={10}
            onChange={handleChange}
            onBlur={handleBlur}
          />
          {!validity.userPhoneNumber && touched.userPhoneNumber && (
            <small className="form-text text-danger">Please enter a valid phone number.</small>
          )}
        </div>
        <p>Already have an account? <a href='/login'>Log In</a></p>
        <button type="button" className="signin-btn" onClick={signIn} disabled={!isFormValid}>Sign In</button>
      </form>
    </div>
  );
}

export default SignUp;