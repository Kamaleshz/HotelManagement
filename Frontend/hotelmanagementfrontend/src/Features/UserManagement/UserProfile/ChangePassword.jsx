import React, { useState } from 'react';
import './ChangePassword.css';
import { toast } from 'react-toastify';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { regExp } from '../Shared/RegExp';

function ChangePasswordModal({ show, handleClose, userId }) {
  const [passwordDetails, setPasswordDetails] = useState({
    userId: userId,
    oldPassword: '',
    newPassword: ''
  });

  const [validity, setValidity] = useState({
    oldPassword: false,
    newPassword: false
  });

  const [touched, setTouched] = useState({
    oldPassword: false,
    newPassword: false
  });

  const [passwordVisible, setPasswordVisible] = useState(false);
  const [oldPasswordVisible, setOldPasswordVisible] = useState(false);

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };

  const toggleOldPasswordVisibility = () => {
    setOldPasswordVisible(!oldPasswordVisible);
  };

  const regexp = regExp();

  const validate = {
    oldPassword: (password) => !!password,
    newPassword: (password) => regexp.PASSWORD_REGEX.test(password)
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setPasswordDetails((prevState) => ({ ...prevState, [name]: value }));
    setValidity((prevState) => ({ ...prevState, [name]: validate[name](value) }));
  };

  const handleBlur = (e) => {
    const { name } = e.target;
    setTouched((prevState) => ({ ...prevState, [name]: true }));
  };

  const handleSaveClick = async () => {
    try {
      const response = await UserManagementService.changePassword(passwordDetails);
      toast.success(response.data);
      handleClose();
    } catch (error) {
      toast.error(error.response.data.error);
    }
  };

  if (!show) return null;

  const isFormValid = Object.values(validity).every(Boolean);

  return (
    <div className="changepasswordmodal-overlay">
      <div className="changepasswordmodal">
        <div className="changepasswordmodal-header">
          <h5>Change Password</h5>
        </div>
        <div className="changepasswordmodal-body">
          <div className="form-group">
            <label htmlFor="formOldPassword">Old Password</label>
            <div className="password-input-container">
              <input
                type={oldPasswordVisible ? 'text' : 'password'}
                className={`form-control ${!validity.oldPassword && touched.oldPassword ? 'is-invalid' : ''}`}
                id="formOldPassword"
                name="oldPassword"
                value={passwordDetails.oldPassword}
                onChange={handleInputChange}
                onBlur={handleBlur}
              />
              <button
                type="button"
                className={`password-toggle-btn ${!validity.oldPassword && touched.oldPassword ? 'invalid' : ''}`}
                onClick={toggleOldPasswordVisibility}
              >
                {oldPasswordVisible ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye-fill"></i>}
              </button>
            </div>
            {!validity.oldPassword && touched.oldPassword && (
              <small className="form-text text-danger">Please enter your old password.</small>
            )}
          </div>
          <div className="form-group">
            <label htmlFor="formNewPassword">New Password</label>
            <div className="password-input-container">
              <input
                type={passwordVisible ? 'text' : 'password'}
                className={`form-control ${!validity.newPassword && touched.newPassword ? 'is-invalid' : ''}`}
                id="formNewPassword"
                name="newPassword"
                value={passwordDetails.newPassword}
                onChange={handleInputChange}
                onBlur={handleBlur}
              />
              <button
                type="button"
                className={`password-toggle-btn ${!validity.newPassword && touched.newPassword ? 'invalid' : ''}`}
                onClick={togglePasswordVisibility}
              >
                {passwordVisible ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye-fill"></i>}
              </button>
            </div>
            {!validity.newPassword && touched.newPassword && (
              <small className="form-text text-danger">
                Password must be between 8 and 15 characters, and contain at least one uppercase letter, one lowercase letter, one number, and one special character.
              </small>
            )}
          </div>
        </div>
        <div className="changepasswordmodal-footer">
          <button type="button" className="btn btn-secondary" onClick={handleClose}>Cancel</button>
          <button type="button" className="btn btn-primary" onClick={handleSaveClick} disabled={!isFormValid}>Save</button>
        </div>
      </div>
    </div>
  );
}

export default ChangePasswordModal;
