import React from "react";
import { useHistory } from "react-router-dom";
import { Button, Dropdown, Menu } from "semantic-ui-react";
import { logoutUser } from "../../authentication/authentication-services";
import { UserGetDto } from "../../constants/types";
import { routes } from "../../routes/config";
import { Navbar } from "../LandingPageNav/landingpagenav";
import "../../components/LandingPageNav/landingpagenav.css";

type PrimaryNavigationProps = {
  user?: UserGetDto;
};

export const PrimaryNavigation: React.FC<PrimaryNavigationProps> = ({
  user,
}) => {
  const history = useHistory();

  return (
    <Menu>
      {user && (
        <>
          <Navbar />
          <Menu.Menu position="right">
            <Dropdown
              item
              className="navbar"
              trigger={
                <span
                  className="user-logo"
                  title={`${user.firstName} ${user.lastName}`}
                >
                  {user.firstName.substring(0, 1).toUpperCase()}
                  {user.lastName.substring(0, 1).toUpperCase()}
                </span>
              }
              icon={null}
            >
              <Dropdown.Menu>
                <Dropdown.Item
                  onClick={async () => {
                    logoutUser();
                  }}
                >
                  Sign Out
                </Dropdown.Item>
                <Dropdown.Item
                  onClick={() => {
                    history.push(routes.users.profile);
                  }}
                >
                  Profile
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </Menu.Menu>
        </>
      )}
    </Menu>
  );
};

export const HomeButton = () => {
  const history = useHistory();

  return <Button onClick={() => history.push(routes.home)}>Home</Button>;
};

export const MealTypesButton = () => {
  const history = useHistory();

  return (
    <Button onClick={() => history.push(routes.mealTypes.listing)}>
      Meal Types
    </Button>
  );
};

export const GroupsButton = () => {
  const history = useHistory();

  return (
    <Button onClick={() => history.push(routes.groups.listing)}>Groups</Button>
  );
};

export const IngredientsButton = () => {
  const history = useHistory();

  return (
    <Button onClick={() => history.push(routes.ingredients.listing)}>
      Ingredients
    </Button>
  );
};

export const ShoppingListsButton = () => {
  const history = useHistory();

  return (
    <Button onClick={() => history.push(routes.shoppingLists.listing)}>
      Shopping Lists
    </Button>
  );
};

export const RecipesButton = () => {
  const history = useHistory();

  return (
    <Button onClick={() => history.push(routes.recipes.listing)}>
      Recipes
    </Button>
  );
};

export const MemberRolesButton = () => {
  const history = useHistory();

  return (
    <Button onClick={() => history.push(routes.memberRoles.listing)}>
      Member Roles
    </Button>
  );
};
