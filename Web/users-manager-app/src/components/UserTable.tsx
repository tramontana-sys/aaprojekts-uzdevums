import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchUsers, updateUser, deleteUser } from '../redux/usersApi';
import { RootState, AppDispatch } from '../redux/store';
import { User } from '../types/User';

interface UserTableProps {
  filteredUsers: User[];
}

const UserTable: React.FC<UserTableProps> = ({ filteredUsers }) => {
  const dispatch = useDispatch<AppDispatch>();
  const { status, error } = useSelector((state: RootState) => state.users);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [itemsPerPage, setItemsPerPage] = useState<number>(5);
  const [nameEdit, setNameEdit] = useState<{ userId: number; value: string } | null>(null);
  const [emailEdit, setEmailEdit] = useState<{ userId: number; value: string } | null>(null);

  useEffect(() => {
    if (status === 'idle') {
      dispatch(fetchUsers());
    }
  }, [status, dispatch]);

  const handleUpdateUser = (user: User) => {
    dispatch(updateUser(user)).then(() => {
      setEditingId(null);
      setNameEdit(null);
      setEmailEdit(null); 
    });
  };

  const handleDeleteUser = (id: number) => {
    dispatch(deleteUser(id));
  };

  const handleNameChange = (userId: number, value: string) => {
    setNameEdit({ userId, value });
  };

  const handleEmailChange = (userId: number, value: string) => {
    setEmailEdit({ userId, value });
  };

  const handleBlur = (user: User) => {
    if (nameEdit?.userId === user.id) {
      handleUpdateUser({ ...user, name: nameEdit.value });
    }
    if (emailEdit?.userId === user.id) {
      handleUpdateUser({ ...user, email: emailEdit.value });
    }
  };

  const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>, user: User) => {
    if (event.key === 'Enter') {
      handleBlur(user);
      dispatch(fetchUsers());
    }
  };

  const indexOfLastUser = currentPage * itemsPerPage;
  const indexOfFirstUser = indexOfLastUser - itemsPerPage;
  const currentUsers = filteredUsers.slice(indexOfFirstUser, indexOfLastUser);

  const totalPages = Math.ceil(filteredUsers.length / itemsPerPage);

  const maxUsersToShow = Math.min(filteredUsers.length, 100);
  const options = Array.from({ length: Math.ceil(maxUsersToShow / 5) }, (_, i) => (i + 1) * 5);

  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Verified</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {currentUsers.map(user => (
            <tr key={user.id}>
              <td onClick={() => setEditingId(user.id)}>
                {editingId === user.id ? (
                  <input
                    type="text"
                    value={nameEdit?.userId === user.id ? nameEdit.value : user.name}
                    onChange={e => handleNameChange(user.id, e.target.value)}
                    // onBlur={() => handleBlur(user)}
                    onKeyDown={(e) => handleKeyDown(e, user)}
                  />
                ) : (
                  user.name
                )}
              </td>
              <td onClick={() => setEditingId(user.id)}>
                {editingId === user.id ? (
                  <input
                    type="text"
                    value={emailEdit?.userId === user.id ? emailEdit.value : user.email}
                    onChange={e => handleEmailChange(user.id, e.target.value)}
                    // onBlur={() => handleBlur(user)}
                    onKeyDown={(e) => handleKeyDown(e, user)}
                  />
                ) : (
                  user.email
                )}
              </td>
              <td>{user.verified ? 'Yes' : 'No'}</td>
              <td>
                <button onClick={() => handleDeleteUser(user.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div>
        <button onClick={() => setCurrentPage(currentPage - 1)} disabled={currentPage === 1}>
          Previous
        </button>
        <span>{`Page ${currentPage} of ${totalPages}`}</span>
        <button onClick={() => setCurrentPage(currentPage + 1)} disabled={currentPage === totalPages}>
          Next
        </button>
        <select value={itemsPerPage} onChange={e => setItemsPerPage(Number(e.target.value))}>
          {options.map(option => (
            <option key={option} value={option}>
              {option}
            </option>
          ))}
        </select>
      </div>
      {status === 'loading' && <p>Loading...</p>}
      {status === 'failed' && <p>{error}</p>}
    </div>
  );
};

export default UserTable;
