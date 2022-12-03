import "./page-wrapper.css";
import React from "react";
import { UserGetDto } from "../../constants/types";
import { PrimaryNavigation } from "../../components/buttons/navigation-buttons";

type PageWrapperProps = {
  user?: UserGetDto;
};

//This is the wrapper that surrounds every page in the app.  Changes made here will be reflect all over.

export const PageWrapper: React.FC<PageWrapperProps> = ({ user, children }) => {
  return (
    <div>
      <PrimaryNavigation user={user} />
      <div>{children}</div>
    </div>
  );
};
