import React, { Component } from 'react';

export class Countries extends Component {
    static displayName = Countries.name;

  constructor(props) {
    super(props);
    this.state = { countries: [], loading: true };
  }

  componentDidMount() {
      this.populateCountryData();
  }

    static renderCountriesTable(countries) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Capital</th>
            <th>Population</th>
            <th>Latitude</th>
            <th>Longitude</th>
          </tr>
        </thead>
        <tbody>
          {countries.map(country =>
            <tr key={country.name}>
                <td>{country.capital}</td>
                <td>{country.population}</td>
                <td>{country.latitude}</td>
                <td>{country.longitude}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : Countries.renderCountriesTable(this.state.countries);

    return (
      <div>
        <h1 id="tabelLabel" >Countries</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

    async populateCountryData() {
        try {
            const response = await fetch('api/countries/top5');
            if (!response.ok) {
                throw new Error('Failed to fetch data');
            }
            const data = await response.json();
            this.setState({ countries: data, loading: false });
        } catch (error) {
            console.error('Error fetching data:', error);
            this.setState({ loading: false }); // Update state to indicate loading has finished
        }
    }
}
