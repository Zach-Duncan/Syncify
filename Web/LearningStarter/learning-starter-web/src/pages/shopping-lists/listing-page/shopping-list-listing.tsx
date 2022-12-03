import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, ShoppingListGetDto } from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import "./shopping-list-listing.css";

export const ShoppingListListingPage = () => {
  const [shoppingLists, setShoppingList] = useState<ShoppingListGetDto[]>();
  const history = useHistory();

  useEffect(() => {
    const fetchShoppingList = async () => {
      const response = await axios.get<ApiResponse<ShoppingListGetDto[]>>(
        `${BaseUrl}/api/shopping-lists`
      );
      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setShoppingList(response.data.data);
      }
    };

    fetchShoppingList();
  }, []);

  return (
    <>
      <Header>Shopping List Items</Header>
      {shoppingLists && (
        <>
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Name</Table.HeaderCell>
                <Table.HeaderCell>Edit Item</Table.HeaderCell>
                <Table.HeaderCell>Delete Item</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {shoppingLists.map((shoppingList) => {
                return (
                  <Table.Row key={shoppingList.id}>
                    <Table.Cell>{shoppingList.id}</Table.Cell>
                    <Table.Cell>{shoppingList.name}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        type="button"
                        content="Edit Item"
                        icon="pencil"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.shoppingLists.update.replace(
                              ":id",
                              `${shoppingList.id}`
                            )
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        type="button"
                        content="Delete Item"
                        icon="trash"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.shoppingLists.delete.replace(
                              ":id",
                              `${shoppingList.id}`
                            )
                          )
                        }
                      />
                    </Table.Cell>
                  </Table.Row>
                );
              })}
            </Table.Body>
          </Table>
        </>
      )}
    </>
  );
};
