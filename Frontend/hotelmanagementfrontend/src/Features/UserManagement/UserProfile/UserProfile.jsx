import React, { useState, useEffect } from 'react';
import './UserProfile.css';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { toast } from 'react-toastify';
import { useSelector } from 'react-redux';
import { regExp } from '../Shared/RegExp';

function UserProfile() {
  const userData = useSelector(state => state.userDetails);

  const [userDetails, setUserDetails] = useState({
    userId: '',
    firstName: '',
    lastName: '',
    userEmail: '',
    userPhoneNumber: ''
  });
  const [editMode, setEditMode] = useState(false);

  const [validity, setValidity] = useState({
    firstName: true,
    lastName: true,
    userEmail: true,
    userPhoneNumber: true
  });

  const [touched, setTouched] = useState({
    firstName: false,
    lastName: false,
    userEmail: false,
    userPhoneNumber: false
  });

  useEffect(() => {
    setUserDetails({
      userId: userData.userId,
      firstName: userData.firstName,
      lastName: userData.lastName,
      userEmail: userData.userEmail,
      userPhoneNumber: userData.userPhoneNumber
    });
  }, [userData]);

  const regexep = regExp();

  const validate = {
    firstName: (name) => name.length > 0,
    lastName: (name) => name.length > 0,
    userEmail: (email) => regexep.EMAIL_REGEX.test(email),
    userPhoneNumber: (number) => regexep.PHONE_REGEX.test(number)
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setUserDetails(prevState => ({ ...prevState, [name]: value }));
    setValidity(prevState => ({ ...prevState, [name]: validate[name](value) }));
  };

  const handleBlur = (e) => {
    const { name } = e.target;
    setTouched(prevState => ({ ...prevState, [name]: true }));
  };

  const handleEditClick = () => {
    setEditMode(true);
  };

  const initialState = {
    userId: userData.userId,
    firstName: userData.firstName,
    lastName: userData.lastName,
    userEmail: userData.userEmail,
    userPhoneNumber: userData.userPhoneNumber
  }

  const handleSaveClick = async () => {
    if(JSON.stringify(userDetails) === JSON.stringify(initialState)){
      toast.error("No changes made");
      return;
    }
    try {
      await UserManagementService.update(userDetails);
      toast.success("Details updated successfully");
      setEditMode(false);
    } catch (error) {
      toast.error(error.response.data.error);
    }
  };

  const isFormValid = Object.values(validity).every(Boolean);

  return (
    <div className="container">
      <h2>{editMode ? 'Edit Profile' : 'User Profile'}</h2>
      <form>
        <div className="form-group">
          <label htmlFor="inputFirstName">First Name</label>
          <input
            type="text"
            className={`form-control ${!validity.firstName && touched.firstName ? 'is-invalid' : ''}`}
            id="inputFirstName"
            name="firstName"
            placeholder="Enter first name"
            value={userDetails.firstName}
            maxLength={15}
            onChange={handleInputChange}
            onBlur={handleBlur}
            readOnly={!editMode}
          />
          {!validity.firstName && touched.firstName && (
            <small className="form-text text-danger">Please enter your first name.</small>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="inputLastName">Last Name</label>
          <input
            type="text"
            className={`form-control ${!validity.lastName && touched.lastName ? 'is-invalid' : ''}`}
            id="inputLastName"
            name="lastName"
            placeholder="Enter last name"
            value={userDetails.lastName}
            maxLength={15}
            onChange={handleInputChange}
            onBlur={handleBlur}
            readOnly={!editMode}
          />
          {!validity.lastName && touched.lastName && (
            <small className="form-text text-danger">Please enter your last name.</small>
          )}
        </div>
        <div className="form-group">
          <label htmlFor="inputEmail">Email address</label>
          <input
            type="email"
            className={`form-control ${!validity.userEmail && touched.userEmail ? 'is-invalid' : ''}`}
            id="inputEmail"
            name="userEmail"
            placeholder="Enter email"
            value={userDetails.userEmail}
            maxLength={30}
            onChange={handleInputChange}
            onBlur={handleBlur}
            readOnly={!editMode}
          />
          {!validity.userEmail && touched.userEmail && (
            <small className="form-text text-danger">Please enter a valid email address.</small>
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
            value={userDetails.userPhoneNumber}
            maxLength={10}
            onChange={handleInputChange}
            onBlur={handleBlur}
            readOnly={!editMode}
          />
          {!validity.userPhoneNumber && touched.userPhoneNumber && (
            <small className="form-text text-danger">Please enter a valid phone number.</small>
          )}
        </div>
        <a href='/'>Update Password</a>
        {editMode ? (
          <div className='edit-buttons'>
          <button
            type="button"
            className="userprofile-btn save-btn"
            onClick={handleSaveClick}
            disabled={!isFormValid}
          >
            Save
          </button>
          <button
            type="button"
            className="userprofile-btn close-btn"
            onClick={() => setEditMode(false)}
          >
            Close
        </button>
        </div>
        ) : (
          <button type="button" className="userprofile-btn btn-primary" onClick={handleEditClick}>
           <i id="userprofile-edit" className="bi bi-pen"></i> Edit
          </button>
        )}
      </form>
    </div>
  );
}

export default UserProfile;
