import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import UserTable from './UserTable';
import UserCreate from './UserCreate';
import UserSearch from './UserSearch';
import { RootState } from '../redux/store';

const Main: React.FC = () => {
  const { users } = useSelector((state: RootState) => state.users);
  const [filter, setFilter] = useState<string>('');

  const filteredUsers = users.filter(user =>
    user.name.toLowerCase().includes(filter.toLowerCase()) ||
    user.email.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <div>
      <UserSearch filter={filter} setFilter={setFilter} />
      <UserTable filteredUsers={filteredUsers} />
      <UserCreate />
    </div>
  );
};

export default Main;
