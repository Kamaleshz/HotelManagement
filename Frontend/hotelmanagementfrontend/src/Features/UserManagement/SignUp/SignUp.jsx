import React, { useState } from 'react';
import './SignUp.css';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

const EMAIL_REGEX = /^[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]{2,}$/;
const PASSWORD_REGEX = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,15}$/;
const PHONE_REGEX = /^\d{10}$/;

function SignUp() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    phoneNumber: ''
  });

  const [validity, setValidity] = useState({
    firstName: false,
    lastName: false,
    email: false,
    password: false,
    phoneNumber: false
  });

  const [touched, setTouched] = useState({
    firstName: false,
    lastName: false,
    email: false,
    password: false,
    phoneNumber: false
  });

  const [passwordVisible, setPasswordVisible] = useState(false);
  const navigate = useNavigate();

  const validate = {
    firstName: (name) => name.length > 0,
    lastName: (name) => name.length > 0,
    email: (email) => EMAIL_REGEX.test(email),
    password: (password) => PASSWORD_REGEX.test(password),
    phoneNumber: (number) => PHONE_REGEX.test(number)
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
    setValidity((prev) => ({ ...prev, [name]: validate[name](value) }));
  };

  const handleBlur = (e) => {
    const { name } = e.target;
    setTouched((prev) => ({ ...prev, [name]: true }));
  };

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };

  const signIn = async () => {
    try {
      await UserManagementService.register({
        firstName: formData.firstName,
        lastName: formData.lastName,
        userEmail: formData.email,
        password: formData.password,
        userPhoneNumber: formData.phoneNumber,
        userRole: 3
      });
      toast.success("Signed Up successfully");
      navigate("/login");
    } catch (error) {
      toast.error(error.response?.data?.error || "Registration failed");
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
            className={`form-control ${!validity.email && touched.email ? 'is-invalid' : ''}`}
            id="inputEmail"
            name="email"
            placeholder="Enter email"
            value={formData.email}
            maxLength={30}
            onChange={handleChange}
            onBlur={handleBlur}
          />
          {!validity.email && touched.email && (
            <small className="form-text text-danger">Please enter a valid email address.</small>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="inputPassword">Password</label>
          <div className="password-input-container">
            <input
              type={passwordVisible ? 'text' : 'password'}
              className={`form-control ${!validity.password && touched.password ? 'is-invalid' : ''}`}
              id="inputPassword"
              name="password"
              placeholder="Password"
              value={formData.password}
              maxLength={15}
              onChange={handleChange}
              onBlur={handleBlur}
            />
            <button
              type="button"
              className={`password-toggle-btn ${!validity.password && touched.password ? 'invalid' : ''}`}
              onClick={togglePasswordVisibility}
            >
              {passwordVisible ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye-fill"></i>}
            </button>
          </div>
          {!validity.password && touched.password && (
            <small className="form-text text-danger">
              Password must be between 8 and 15 characters, and contain at least one uppercase letter, one lowercase letter, one number, and one special character.
            </small>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="inputPhoneNumber">Phone Number</label>
          <input
            type="tel"
            className={`form-control ${!validity.phoneNumber && touched.phoneNumber ? 'is-invalid' : ''}`}
            id="inputPhoneNumber"
            name="phoneNumber"
            placeholder="Enter phone number"
            value={formData.phoneNumber}
            maxLength={10}
            onChange={handleChange}
            onBlur={handleBlur}
          />
          {!validity.phoneNumber && touched.phoneNumber && (
            <small className="form-text text-danger">Please enter a valid phone number.</small>
          )}
        </div>
        <button type="button" className="signin-btn" onClick={signIn} disabled={!isFormValid}>Sign In</button>
      </form>
    </div>
  );
}

export default SignUp;
