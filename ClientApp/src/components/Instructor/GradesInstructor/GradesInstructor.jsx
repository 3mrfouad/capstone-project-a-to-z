import React from "react";
import BootstrapTable from "react-bootstrap-table-next";
import cellEditFactory from "react-bootstrap-table2-editor";
//this is for test

const columns = [
  {
    dataField: "id",
    text: "Product ID",
  },
  {
    dataField: "name",
    text: "Product Name",
    // Product Name column can't be edit anymore
    editable: false,
  },
  {
    dataField: "price",
    text: "Product Price",
  },
];
const products = [
  { id: 1, name: "George", price: "Monkey" },
  { id: 2, name: "Jeffrey", price: "Giraffe" },
  { id: 3, name: "Alice", price: "Giraffe" },
  { id: 4, name: "Alice", price: "Tiger" },
];

const GradesStudent = () => {
  return (
    <React.Fragment>
      <BootstrapTable
        keyField="id"
        data={products}
        columns={columns}
        cellEdit={cellEditFactory({
          mode: "click",
          blurToSave: true,
        })}
      />
    </React.Fragment>
  );
};

export default GradesStudent;
