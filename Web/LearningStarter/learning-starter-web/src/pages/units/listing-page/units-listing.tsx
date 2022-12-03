import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, UnitGetDto } from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import "./units-listing.css";

export const UnitListingPage = () => {
  const [units, setUnit] = useState<UnitGetDto[]>();
  const history = useHistory();

  useEffect(() => {
    const fetchUnit = async () => {
      const response = await axios.get<ApiResponse<UnitGetDto[]>>(
        `${BaseUrl}/api/units`
      );
      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setUnit(response.data.data);
      }
    };

    fetchUnit();
  }, []);

  return (
    <>
      {units && (
        <>
          <Header>Units</Header>
          <Button
            positive
            icon="circle plus"
            content="Create"
            labelPosition="left"
            onClick={() => history.push(routes.units.create)}
          />
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Name</Table.HeaderCell>
                <Table.HeaderCell>Abbreviation</Table.HeaderCell>
                <Table.HeaderCell>Edit Unit</Table.HeaderCell>
                <Table.HeaderCell>Delete Unit</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {units.map((unit) => {
                return (
                  <Table.Row key={unit.id}>
                    <Table.Cell>{unit.id}</Table.Cell>
                    <Table.Cell>{unit.name}</Table.Cell>
                    <Table.Cell>{unit.abbreviation}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        type="button"
                        content="Edit Unit"
                        icon="pencil"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.units.update.replace(":id", `${unit.id}`)
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        type="button"
                        content="Delete"
                        icon="trash"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.units.delete.replace(":id", `${unit.id}`)
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
