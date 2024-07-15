import React from 'react';

interface UserSearchProps {
  filter: string;
  setFilter: (filter: string) => void;
}

const UserSearch: React.FC<UserSearchProps> = ({ filter, setFilter }) => {
  return (
    <div>
      <input 
        className="searchInput"
        type="text"
        placeholder="Search"
        value={filter}
        onChange={e => setFilter(e.target.value)}
      />
    </div>
  );
};

export default UserSearch;
