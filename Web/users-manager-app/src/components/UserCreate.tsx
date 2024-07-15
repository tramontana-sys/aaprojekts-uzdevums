import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { createUser } from '../redux/usersApi';
import { AppDispatch } from '../redux/store';
import { User } from '../types/User';

const UserCreate: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const [newUser, setNewUser] = useState<User>({ id: 0, name: '', email: '', verified: false });

  const handleCreateUser = () => {
    dispatch(createUser(newUser));
    setNewUser({ id: 0, name: '', email: '', verified: false });
  };

  return (
    <div>
      <input
        type="text"
        placeholder="Name"
        value={newUser.name}
        onChange={e => setNewUser({ ...newUser, name: e.target.value })}
      />
      <input
        type="text"
        placeholder="Email"
        value={newUser.email}
        onChange={e => setNewUser({ ...newUser, email: e.target.value })}
      />
      <button onClick={handleCreateUser}>Add User</button>
    </div>
  );
};

export default UserCreate;
