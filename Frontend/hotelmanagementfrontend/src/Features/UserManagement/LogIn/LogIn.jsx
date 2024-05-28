import React, { useState } from 'react';
import './Login.css';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setUserDetails } from '../../State/UserDetails.actions';
import { regExp } from '../Shared/RegExp';

function LoginPopup() {
  const [show, setShow] = useState(false);
  const [passwordVisible, setPasswordVisible] = useState(false);

  const [formData, setFormData] = useState({
    email:'',
    password:''
  })

  const [validity, setValidity] = useState({
    email: false,
    password: false
  })

  const [touched, setTouched] = useState({
    email: false,
    password: false
  })

  const regexes = regExp();

  const validate = {
    email: (email) => regexes.EMAIL_REGEX.test(email),
    password: (password) => !!password
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData ((prevState) => ({ ...prevState, [name]: value }));
    setValidity ((prevState) => ({ ...prevState, [name]: validate[name](value) })); 
  };

  const handleBlur = (e) => {
    const { name }  = e.target;
    setTouched((prevState) => ({...prevState, [name]: true }));
  };  

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  }

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleClose = () => {
    setShow(false);
  };

  const handleShow = () => {
    setShow(true);
  };

  const login = async () => {
    try {
      const response = await UserManagementService.login({ userEmail: formData.email, password: formData.password });
        if(response.success)
          {
            toast.success("Logged In successfully")
            dispatch(setUserDetails(response.data));
            navigate("/userprofile");
            handleClose();
          }
    } catch (error) {
      toast.error(error.response.data.error);
    }
  };

  const isFormValid = Object.values(validity).every(Boolean);

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
                      className={`form-control ${!validity.email && touched.email ? 'is-invalid' : ''}`}
                      name="email"
                      aria-describedby="emailHelp"
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
                    <div className='login-password-input-container'>
                    <input
                      type={passwordVisible ? 'text' : 'password'}
                      className={`form-control ${!validity.password && touched.password ? 'is-invalid' : ''}`}
                      name="password"
                      placeholder="Password"
                      value={formData.password}
                      maxLength={15}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    <button
                      type="button"
                      className={`login-password-toggle-btn ${!validity.password && touched.password ? 'invalid': `` }`}
                      onClick={togglePasswordVisibility}
                    >
              {passwordVisible ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye-fill"></i>}
            </button>
                    { !validity.password && touched.password && (
                      <small className='form-text text-danger'>Please enter the password.</small>
                    )}
                  </div>
                  </div>
                  <p>Don't have an account? <a href='/signup'>Sign Up</a></p>
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
