import React, { useState, useEffect } from 'react';
import './UserProfile.css';
import UserManagementService from '../../../Services/UserManagementApiCalls';
import { ToastContainer, toast } from 'react-toastify';

function UserProfile() {
  const [userDetails, setUserDetails] = useState({
    firstName: '',
    lastName: '',
    userEmail: '',
    userPhoneNumber: ''
  });
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    fetchUserDetails();
  }, []);

  const fetchUserDetails = async () => {
    try {
      const userData = await UserManagementService.getById(9);
      setUserDetails(userData.data);
    } catch (error) {
      console.error('Error fetching user details:', error);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setUserDetails(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleEditClick = () => {
    setEditMode(true);
  };

  const handleSaveClick = async () => {
    try {
      await UserManagementService.update(userDetails);
      toast.success("Details updated successfully");
      setEditMode(false);
    } catch (error) {
      console.error('Error updating user details:', error);
      toast.error("Failed to update details. Please try again.");
    }
  };

  return (
    <div className="container">
      <h2>{editMode ? 'Edit Profile' : 'User Profile'}</h2>
      <form>
        <div className="form-group">
          <label htmlFor="inputFirstName">First Name</label>
          <input
            type="text"
            className="form-control"
            id="inputFirstName"
            name="firstName"
            placeholder="Enter first name"
            value={userDetails.firstName}
            onChange={handleInputChange}
            readOnly={!editMode}
          />
        </div>
        <div className="form-group">
          <label htmlFor="inputLastName">Last Name</label>
          <input
            type="text"
            className="form-control"
            id="inputLastName"
            name="lastName"
            placeholder="Enter last name"
            value={userDetails.lastName}
            onChange={handleInputChange}
            readOnly={!editMode}
          />
        </div>
        <div className="form-group">
          <label htmlFor="inputEmail">Email address</label>
          <input
            type="email"
            className="form-control"
            id="inputEmail"
            name="email"
            placeholder="Enter email"
            value={userDetails.userEmail}
            onChange={handleInputChange}
            readOnly={!editMode}
          />
        </div>
        {/* <div className="form-group">
          <label htmlFor="inputPassword">Password</label>
          <input
            type="password"
            className="form-control"
            id="inputPassword"
            name="password"
            placeholder="Password"
            value={userDetails.password}
            onChange={handleInputChange}
            readOnly={!editMode}
          />
        </div> */}
        <div className="form-group">
          <label htmlFor="inputPhoneNumber">Phone Number</label>
          <input
            type="tel"
            className="form-control"
            id="inputPhoneNumber"
            name="phoneNumber"
            placeholder="Enter phone number"
            value={userDetails.userPhoneNumber}
            onChange={handleInputChange}
            readOnly={!editMode}
          />
        </div>
        {editMode ? (
          <button type="button" className="btn btn-primary" onClick={handleSaveClick}>
            Save
          </button>
        ) : (
          <button type="button" className="btn btn-primary" onClick={handleEditClick}>
            Edit
          </button>
        )}
      </form>
      <ToastContainer />
    </div>
  );
}

export default UserProfile;
