import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Header, Table } from "semantic-ui-react";
import { ApiResponse, EventGetDto } from "../../../constants/types";
import { BaseUrl } from "../../../constants/env-cars";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import "./events-listing.css";

export const EventListingPage = () => {
  const [events, setEvents] = useState<EventGetDto[]>();
  const history = useHistory();

  useEffect(() => {
    const fetchEvents = async () => {
      const response = await axios.get<ApiResponse<EventGetDto[]>>(
        `${BaseUrl}/api/events`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setEvents(response.data.data);
      }
    };

    fetchEvents();
  }, []);

  return (
    <>
      {events && (
        <>
          <Header>Events</Header>
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Name</Table.HeaderCell>
                <Table.HeaderCell>Event Details</Table.HeaderCell>
                <Table.HeaderCell>Start Date</Table.HeaderCell>
                <Table.HeaderCell>End Date</Table.HeaderCell>
                <Table.HeaderCell>Group Calendar</Table.HeaderCell>
                <Table.HeaderCell>Edit Event</Table.HeaderCell>
                <Table.HeaderCell>Delete Event</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {events.map((event) => {
                return (
                  <Table.Row key={event.id}>
                    <Table.Cell>{event.id}</Table.Cell>
                    <Table.Cell>{event.name}</Table.Cell>
                    <Table.Cell>{event.eventDetails}</Table.Cell>
                    <Table.Cell>{event.startDate}</Table.Cell>
                    <Table.Cell>{event.endDate}</Table.Cell>
                    <Table.Cell>{event.calendar.group.name}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        type="button"
                        content="Edit Event"
                        icon="pencil"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.events.update.replace(":id", `${event.id}`)
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        type="button"
                        content="Delete Event"
                        icon="trash"
                        labelPosition="left"
                        onClick={() =>
                          history.push(
                            routes.events.delete.replace(":id", `${event.id}`)
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

//     <>
//         {events ? (
//           events.map((events) => {
//             return (
//               <Segment>
//                 <div>CalenderId: {events.calendar}</div>
//                 <div>Id: {events.id}</div>
//                 <div>Title: {events.name}</div>
//                 <div>Description: {events.eventDetails}</div>
//               </Segment>
//             );
//           })
//         ) : (
//           <div>Loading</div>
//         )}
//       </div>
//     </>
//   );
// };
