import React from "react";
import { Table, Container, Button } from "react-bootstrap";

const GradingSummary = () => {
  return (
    <React.Fragment>
      <Container>
        <Table>
          <thead>
            <tr>
              <th>Student Name</th>
              <th>Total Grade</th>
              <th>Mandatory</th>
              <th>Challenges</th>
              <th>Timesheet</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Student A</td>
              <td>10/10</td>
              <td>8/8</td>
              <td>2/2</td>
              <td>1/2</td>
              <td>Edit | Grade</td>
            </tr>
            <tr>
              <td>Student B</td>
              <td>10/10</td>
              <td>8/8</td>
              <td>2/2</td>
              <td>1/2</td>
              <td>Edit | Grade</td>
            </tr>
          </tbody>
        </Table>
        <Button>Back</Button> <Button>Create</Button>
      </Container>
    </React.Fragment>
  );
};

export default GradingSummary;
